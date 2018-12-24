using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Logging;
using DapperService;
using DTO;
using Quartz;
using log4net;


namespace Test
{
    public class DownloadOrdersJob : IJob
    {
        // 获取有赞订单同步至ERP
        YzShopService yzService = new YzShopService();
        YzOrderService orderService = new YzOrderService();
        YzExpressageService expService = new YzExpressageService();
        log4net.ILog log = log4net.LogManager.GetLogger(typeof(DownloadOrdersJob));
        private int count=0;
        public void Execute(IJobExecutionContext context)
        {
            log.Debug("准备开始有赞订单同步至ERP"+DateTime.Now);
            try
            {
                
                DownloadOrders();
                log.Debug("有赞订单同步至ERP完成"+DateTime.Now);
            }
            catch (Exception e)
            {
                log.Error("执行AddExpressJob出错" + e);
            }

        }

        public void DownloadOrders()
        {
            count = 0;
            string token = yzService.QueryToken();
            int pageNo = 1;
            if (token != null)
            {
                bool flag = true;
                while (flag)
                {
                    OrderRequest or = new OrderRequest()
                    {
                        start_created = DateTime.Now.AddMonths(-1),
                        end_created = DateTime.Now,
                        page_no = pageNo,
                        page_size = 100,
                        status = "WAIT_SELLER_SEND_GOODS"
                    };
                    OrderResponse orders = yzService.GetOrder(or, token);
                    if (orders != null)
                    {
                        if (orders.response.total_results == 0)
                        {
                            break;
                        }

                        if (orders.response.total_results > 0 && orders.response.total_results <= 100)
                        {
                            if (orders.response.total_results == 100)
                            {
                                pageNo++;
                            }
                            else
                            {
                                //100条内 循环完退出
                                flag = false;
                            }
                            foreach (var order in orders.response.full_order_info_list)
                            {
                                if (orderService.QueryOrderIsExit(order.full_order_info.order_info.tid))
                                {
                                    continue;
                                }

                                if (order.full_order_info.order_info.status_str == "待发货")
                                {
                                    string orderIds = yzService.AddOrder(order);
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
                                        //更新订单来源为有赞
                                        orderService.UpdateOrderSourceTypeID(listOrderId);
                                        if (listOrderId.Count == 1)
                                        {
                                            expService.InsertExpressage(listOrderId[0], " ");
                                            count++;
                                        }
                                        else
                                        {
                                            Dictionary<string, string> dicOrder = new Dictionary<string, string>();
                                            foreach (var orderId in listOrderId)
                                            {
                                                List<OrderList> orderList = new List<OrderList>();
                                                var listItems = orderService.QueryDTOrder(orderId);
                                                foreach (var itemId in listItems)
                                                {
                                                    var ol = order.full_order_info.orders.Where(o => (o.outer_sku_id.Substring(o.outer_sku_id.LastIndexOf("|") + 1, o.outer_sku_id.Length - o.outer_sku_id.LastIndexOf("|") - 1)) == itemId).SingleOrDefault();
                                                    orderList.Add(new OrderList { itemid = itemId, oid = ol.oid });
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
                }
            }

            if (count > 0)
            {
                log.Debug("有赞订单同步至ERP:" + count + "条数据同步成功");
            }

          
        }
    }
}
