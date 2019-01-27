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
    public class AddExpressJob : IJob
    {
        private YzShopService shopService = new YzShopService();
        private OrderService orderService = new OrderService();
        private YzExpressageService expService = new YzExpressageService();
        private static log4net.ILog log = log4net.LogManager.GetLogger(typeof(AddExpressJob));
        private string successUploadOrders=string.Empty;
        private string failUploadOrders = string.Empty;
      
        public void Execute(IJobExecutionContext context)
        {
           
            log.Debug("准备开始ERP发货回传至有赞店铺" + DateTime.Now);
            try
            {
                AddExp();
                log.Debug("ERP发货回传至有赞店铺结束" + DateTime.Now);
            }
            catch (Exception e)
            {
                log.Error("执行AddExpressJob出错" , e);
            }
        }

        public void AddExp()
        {
            string token = shopService.QueryToken();
            if (token != null)
            {
                List<string> sourceIds = new List<string>();
                var models = orderService.QueryOrder("ZHGS23", "ZY0112", ref sourceIds);
                //获取有赞状态为未发货的订单号 sourceids
                var yzSourceIds = shopService.GetWaitSendOid(token);
                //取交集
                var jjSourceIds = sourceIds.Intersect(yzSourceIds).ToList();
                var selModels = models.Where(m => jjSourceIds.Contains(m.SourceOrderID)).ToList();
                //匹配物流信息
                if ( selModels.Count > 0)
                {
                    var listExpress = expService.GetExpress(token);
                    foreach (var model in selModels)
                    {
                        if (model.ExpressName == "EMS快递包裹")
                        {
                            model.ExpressName = "EMS经济快递";
                        }
                        if (model.ExpressName.Contains("中通"))
                        {
                            model.ExpressName = "中通快递";
                        }
                        if (model.ExpressName.Contains("圆通"))
                        {
                            model.ExpressName = "圆通速递";
                        }
                        if (model.ExpressName.Contains("韵达"))
                        {
                            model.ExpressName = "韵达快递";
                        }
                        if (model.ExpressName.Contains("申通"))
                        {
                            model.ExpressName = "申通快递";
                        }

                        var expItem = listExpress.Where(e => e.name == model.ExpressName).ToList();
                        if (expItem.Count > 0)
                        {
                            model.ExpressID = expItem[0].id;
                        }
                        else
                        {
                            log.Error("未匹配到第三方平台的快递类型:"+model.ExpressName);
                        }

                    }
               
                    //回传快递单号
                    foreach (var sourceId in jjSourceIds)
                    {
                        var model = selModels.Where(m => m.SourceOrderID == sourceId).ToList();
                        if (model.Count > 1)
                        {
                            //拆单的
                            foreach (var mod in model)
                            {
                                List<OrderList> listOrder = CommonHelper.DeJson<List<OrderList>>(mod.OrderDTList);
                                string oids = string.Empty;
                                foreach (var item in listOrder)
                                {
                                    oids += item.oid + ",";
                                }
                                oids = oids.Substring(0, oids.Length - 1);
                                //回传物流单号
                                if (!string.IsNullOrEmpty(mod.WAYBILLID))
                                {
                                    if (expService.AddExpress(token, mod.SourceOrderID, oids, mod.WAYBILLID,
                                        mod.ExpressID.ToString(), ""))
                                    {
                                        successUploadOrders += mod.BillID + ",";
                                    }
                                    else
                                    {
                                        failUploadOrders += mod.BillID + ",";
                                    }
                                }
                                else
                                {
                                    failUploadOrders+= mod.BillID + ",";
                                }
                            }
                        }
                        else if (model.Count == 1)
                        {
                            //未拆单
                            if (!string.IsNullOrEmpty(model[0].WAYBILLID))
                            {
                                if (expService.AddExpress(token, model[0].SourceOrderID, "", model[0].WAYBILLID,
                                    model[0].ExpressID.ToString(), ""))
                                {
                                    successUploadOrders += model[0].BillID + ",";
                                }
                                else
                                {
                                    failUploadOrders += model[0].BillID + ",";
                                }
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(successUploadOrders))
                    {
                        log.Debug("===========已将以下订单同步至有赞==========="+ successUploadOrders);
                    }

                    if (!string.IsNullOrEmpty(failUploadOrders))
                    {
                        log.Error("以下订单同步失败 请检查" + failUploadOrders);
                    }
                }
           
            }
            else
            {
                log.Error("未获取到Token码!");
            }
        }

    }

  

}
