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
    public class BbExpressJob : IJob
    {
        private BbShopService shopService = new BbShopService();
        private OrderService orderService = new OrderService();
        private static log4net.ILog log = log4net.LogManager.GetLogger(typeof(BbExpressJob));
        private string successUploadOrders = string.Empty;
        private string failUploadOrders = string.Empty;
        public void Execute(IJobExecutionContext context)
        {
            log.Debug("准备开始ERP发货回传至贝贝店铺" + DateTime.Now);
            try
            {
                AddExp();
                log.Debug("ERP发货回传至贝贝店铺结束" + DateTime.Now);
            }
            catch (Exception e)
            {
                log.Error("执行BbExpressJob出错", e);
            }
        }

        public async void AddExp()
         {
            try
            {
                List<string> sourceIds = new List<string>();
                var models = orderService.QueryOrder("ZHGS23", "ZY0113", ref sourceIds);
                var bbSourceIds = await shopService.GetWaitSendOid();
                var jjSourceIds = sourceIds.Intersect(bbSourceIds).ToList();
                var selModels = models.Where(m => jjSourceIds.Contains(m.SourceOrderID)).ToList();
                if (selModels.Count > 0)
                {
                    var listExpress = await shopService.GetExpress();
                    foreach (var model in selModels)
                    {
                        if (model.ExpressName.Contains("中通"))
                        {
                            model.ExpressName = "中通";
                        }
                        if (model.ExpressName.Contains("圆通"))
                        {
                            model.ExpressName = "圆通";
                        }
                        if (model.ExpressName.Contains("韵达"))
                        {
                            model.ExpressName = "韵达";
                        }
                        if (model.ExpressName.Contains("申通"))
                        {
                            model.ExpressName = "申通";
                        }
                        if (model.ExpressName.Contains("EMS"))
                        {
                            model.ExpressName = "邮政国内";
                        }
                        var expItem = listExpress.ToList().Where(e => e.Name == model.ExpressName).ToList();
                        if (expItem.Count > 0)
                        {
                            model.ExpressCode = expItem[0].Code;
                        }
                    }

                    foreach (var sourceId in jjSourceIds)
                    {
                        var model = selModels.Where(m => m.SourceOrderID == sourceId).ToList();
                        if (model.Count > 0)
                        {
                            foreach (var mod in model)
                            {
                                if (!string.IsNullOrEmpty(mod.WAYBILLID))
                                {
                                    BbExpressRequest request = new BbExpressRequest
                                    {
                                        Oid = sourceId,
                                        Company = mod.ExpressCode,
                                        Out_sid = mod.WAYBILLID,
                                        Order_items = mod.OrderDTList
                                    };
                                    var msg = await shopService.AddExpress(request);
                                    if (msg.Success)
                                    {
                                        successUploadOrders += mod.BillID + ",";
                                    }
                                    else
                                    {
                                        failUploadOrders += mod.BillID + ","+msg.Message;
                                    }
                                }
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(successUploadOrders))
                    {
                        log.Debug("=========已将以下订单同步至贝贝==========" + successUploadOrders);
                    }

                    if (!string.IsNullOrEmpty(failUploadOrders))
                    {
                        log.Error("===========以下订单同步失败 请检查==========" + failUploadOrders);
                    }
                }
            }
            catch (Exception e)
            {

                log.Error("执行AddExp出错", e);
            }
        }
    }
}