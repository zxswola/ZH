using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common;
using DapperService;
using DTO;
using Quartz;

namespace ZSZAdminWeb.Jobs
{
    public class BbDownloadOrderJob : IJob
    {
        private BbShopService bbShopService = new BbShopService();
        private OrderService orderService = new OrderService();
        private ExpressageService expService = new ExpressageService();
        log4net.ILog log = log4net.LogManager.GetLogger(typeof(BbDownloadOrderJob));
        private int count ;
        public void Execute(IJobExecutionContext context)
        {
            log.Debug("准备开始贝店订单同步至ERP" + DateTime.Now);

            try
            {
                DownloadOrders();
                log.Debug("贝店订单同步至ERP完成" + DateTime.Now);
            }
            catch (Exception e)
            {
                log.Error("执行BbDownloadOrderJob出错" + e);
            }
        }

        public async void DownloadOrders()
        {
            try
            {
                count = 0;
                bool flag = true;
                int pageNo = 1;
                while (flag)
                {
                    BbOrderRequest or = new BbOrderRequest
                    {
                        Status = 1,
                        TimeRange = "pay_time",
                        StartTime = DateTime.Now.AddDays(-27),
                        EndTime = DateTime.Now,
                        PageNo = pageNo,
                        PageSize = 300
                    };
                    var orders = await bbShopService.GetOrder(or);
                    if (orders != null)
                    {
                        if (orders.Count == 0)
                        {
                            break;
                        }

                        if (orders.Count > 0)
                        {
                            if (orders.Count == 300)
                            {
                                pageNo++;
                            }
                            else
                            {
                                //退出循环
                                flag = false;
                            }

                            foreach (var order in orders.Data)
                            {
                                if (orderService.QueryOrderIsExit(order.Oid))
                                {
                                    continue;
                                }
                                //orderIds 为ERP系统返回的单号 如EO1812027249,EO1812027250,
                                string orderIds = bbShopService.AddOrder(order);
                                if (!string.IsNullOrEmpty(orderIds))
                                {
                                    List<string> listorderId = orderIds.Split(',').ToList();
                                    List<string> listOrderId = new List<string>();
                                    foreach (var orderId in listorderId)
                                    {
                                        if (!string.IsNullOrEmpty(orderId))
                                        {
                                            //快递中间表没数据
                                            if (!expService.IsExit(orderId))
                                            {
                                                listOrderId.Add(orderId);
                                            }
                                        }
                                    }
                                    if (listOrderId.Count == 0)
                                    {
                                        continue;
                                    }
                                    //更新订单来源为贝贝
                                    orderService.UpdateOrderSourceTypeID(listOrderId, "031");
                                    if (listOrderId.Count == 1)
                                    {
                                        //没拆单
                                        expService.InsertExpressage(listOrderId[0], " ");
                                        count++;
                                    }
                                    else
                                    {
                                        //拆单
                                        Dictionary<string, string> dicOrder = new Dictionary<string, string>();
                                        foreach (var orderId in listOrderId)
                                        {
                                            List<BbOrderItem> orderList = new List<BbOrderItem>();
                                            foreach (var item in order.Item)
                                            {
                                                orderList.Add(new BbOrderItem { Num = item.Num, Outer_id = item.Iid });
                                            }
                                            dicOrder.Add(orderId, CommonHelper.ToJson(orderList));
                                        }
                                        expService.InsertExpressageAll(dicOrder);
                                        count++;
                                    }
                                }
                            }
                        }
                    }
                }
                if (count > 0)
                {
                    log.Debug("===============贝店订单同步至ERP:" + count + "条数据同步成功=========================");
                }
            }
            catch (Exception e)
            {
                log.Error("执行BbDownloadOrderJob出错" + e);
            }

        }
    }

}