using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Common;
using DTO;
using IService;

namespace DapperService
{
    public class BbShopService : IBbShopService
    {
        private static string secret = System.Configuration.ConfigurationManager.AppSettings["bbSecret"];
        private static string appId = System.Configuration.ConfigurationManager.AppSettings["bbAppId"];
        private static string session = System.Configuration.ConfigurationManager.AppSettings["bbSession"];
        private static string gateway = System.Configuration.ConfigurationManager.AppSettings["bbGateway"];

        private static string appkey = System.Configuration.ConfigurationManager.AppSettings["bb_appkey"];
        private static string appsecret = System.Configuration.ConfigurationManager.AppSettings["bb_appsecret"];
        private OrderService orderService = new OrderService();
        private static log4net.ILog log = log4net.LogManager.GetLogger(typeof(BbShopService));
        public ILogService logService = new LogService();
        //调用ZH接口发送增加订单
        public string AddOrder(BbOrder orderItem)
        {
            try
            {
                List<GoodsItem> listGoodsItem = new List<GoodsItem>();
                SortedDictionary<string, string> sb = new SortedDictionary<string, string>();
                sb.Add("address", orderItem.Address);
                sb.Add("city", orderItem.City);
                foreach (var item in orderItem.Item)
                {
                    listGoodsItem.Add(new GoodsItem
                    {
                        item_id = orderService.QueryBarcode(item.Outer_id),
                        qty = Convert.ToInt32(item.Num),
                        price = Convert.ToDouble(item.Total_fee/ Convert.ToInt32(item.Num))
                    });
                }
                string goodsJson = CommonHelper.ToJson(listGoodsItem);
                sb.Add("items", goodsJson);
                sb.Add("mobile", orderItem.Receiver_phone);
                sb.Add("order_created", orderItem.Pay_time);
                sb.Add("order_id", orderItem.Oid);
                sb.Add("province", orderItem.Province);
                sb.Add("remark", orderItem.Remark);
                sb.Add("recevier", orderItem.Receiver_name);
                sb.Add("tel", orderItem.Receiver_phone);
                sb.Add("town", orderItem.County);
                string ordersJosonStr = HttpHepler.UrlResponseByPost("add_order", sb, appkey, appsecret);
                if (!string.IsNullOrEmpty(ordersJosonStr))
                {
                    RootResponse rootModel = CommonHelper.DeJson<RootResponse>(ordersJosonStr);
                    if (rootModel.Code == "0")
                    {
                        return rootModel.AddOrderResponse.ERPORDERID;
                    }
                    logService.AddLog("贝店订单号:"+ orderItem.Oid+"  收件人信息:"+orderItem.Receiver_name+orderItem.Receiver_phone +"  地址:"+ orderItem.Province + orderItem.City + orderItem.County + orderItem.Address+" 错误信息:"+ rootModel.ErrorMessage.Trim());
                    log.Error(rootModel.ErrorMessage);
                }
                return null;
            }
            catch (Exception e)
            {
               
                throw e ;
            }
        }

        public string QueryGoods()
        {
            SortedDictionary<string, string> sb = new SortedDictionary<string, string>();
            sb.Add("page", "1");
            sb.Add("pagesize", "50");
            string jsonStr = HttpHepler.UrlResponseByPost("goods_query_full", sb, appkey, appsecret);
            return jsonStr;
        }

        public async Task<BbExpressResponse> AddExpress(BbExpressRequest request)
        {
            try
            {
                using (HttpClient hc = new HttpClient())
                {
                    Dictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("method", "beibei.outer.trade.logistics.ship");
                    dict.Add("app_id", appId);
                    dict.Add("session", session);
                    dict.Add("timestamp", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    dict.Add("oid", request.Oid);
                    dict.Add("company", request.Company);
                    dict.Add("out_sid", request.Out_sid);
                    if (request.Order_items != null)
                    {
                        dict.Add("order_items", request.Order_items);
                    }
                    string sing = CommonHelper.BbOpera(dict, secret);
                    dict.Add("sign", sing);
                    FormUrlEncodedContent content = new FormUrlEncodedContent(dict);
                    HttpResponseMessage msg = await hc.PostAsync(gateway, content);
                    string res = await msg.Content.ReadAsStringAsync();
                    return CommonHelper.DeJson<BbExpressResponse>(res);
                }
            }
            catch (Exception e)
            {
                throw e ;
            }
        }

        /// <summary>
        /// 获取所有的物流信息
        /// </summary>
        /// <returns></returns>
        public async Task<BbCompany[]> GetExpress()
        {
            try
            {
                using (HttpClient hc = new HttpClient())
                {
                    Dictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("method", "beibei.outer.logistics.company.get");
                    dict.Add("app_id", appId);
                    dict.Add("session", session);
                    dict.Add("timestamp", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    dict.Add("version", "1.0");
                    string sing = CommonHelper.BbOpera(dict, secret);
                    dict.Add("sign", sing);
                    FormUrlEncodedContent content = new FormUrlEncodedContent(dict);
                    HttpResponseMessage msg = await hc.PostAsync(gateway, content);
                    string res = await msg.Content.ReadAsStringAsync();
                    var companys = CommonHelper.DeJson<BbCompanyResponse>(res);
                    if (companys.Success)
                    {
                        return companys.Data;
                    }
                    return null;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 获取贝贝网店订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<BbOrderResponse> GetOrder(BbOrderRequest request)
        {
            try
            {
                string method = "beibei.outer.trade.order.get";
                using (HttpClient hc = new HttpClient())
                {
                    Dictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("method", method);
                    dict.Add("app_id", appId);
                    dict.Add("session", session);
                    dict.Add("timestamp", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    dict.Add("time_range", request.TimeRange);
                    dict.Add("start_time", request.StartTime.ToString("yyyy-MM-dd HH:mm:ss"));
                    dict.Add("end_time", request.EndTime.ToString("yyyy-MM-dd HH:mm:ss"));
                    //待发货
                    dict.Add("status", request.Status.ToString());
                    dict.Add("page_no", request.PageNo.ToString());
                    dict.Add("page_size", request.PageSize.ToString());
                    string sing = CommonHelper.BbOpera(dict, secret);
                    dict.Add("sign", sing);
                    FormUrlEncodedContent content = new FormUrlEncodedContent(dict);
                    HttpResponseMessage msg = await hc.PostAsync(gateway, content);
                    string res = await msg.Content.ReadAsStringAsync();
                    var resOrders = CommonHelper.DeJson<BbOrderResponse>(res);
                    return resOrders;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
         
        }
        //获取待发货的订单号
        public async Task<List<string>> GetWaitSendOid()
        {
            try
            {
                List<string> sourceIds = new List<string>();
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
                    var orders = await GetOrder(or);
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
                                sourceIds.Add(order.Oid);
                            }
                        }
                    }
                }

                return sourceIds;
            }
            catch (Exception e)
            {
                log.Error(e);
                throw e;
            }
        }

        public async Task<BbItemResponse> GetItemResponse(int pageNo, int pageSize)
        {
            try
            {
                string method = "beibei.outer.item.onsale.get";
                using (HttpClient hc = new HttpClient())
                {
                    Dictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("method", method);
                    dict.Add("app_id", appId);
                    dict.Add("secret", secret);
                    dict.Add("session", session);
                    dict.Add("timestamp", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    dict.Add("version", "1.0");
                    dict.Add("page_no", pageNo.ToString());
                    dict.Add("page_size", pageSize.ToString());
                    string sing = CommonHelper.BbOpera(dict, secret);
                    dict.Add("sign", sing);
                    FormUrlEncodedContent content = new FormUrlEncodedContent(dict);
                    HttpResponseMessage msg = await hc.PostAsync(gateway, content);
                    string res = await msg.Content.ReadAsStringAsync();
                    var resItems = CommonHelper.DeJson<BbItemResponse>(res);
                    return resItems;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<BbItemDetail>> GetListItem()
        {
            try
            {
                List<BbItemDetail> listItem = new List<BbItemDetail>();
                bool flag = true;
                int pageNo = 1;
                int pageSize = 200;
                while (flag)
                {
                    BbItemResponse items = await GetItemResponse(pageNo, pageSize);
                    if (items != null)
                    {
                        if (items.Success)
                        {
                            if (items.Count > 0)
                            {
                                listItem.AddRange(items.Data);
                            }
                            //还有数据 pageno加1
                            if (items.Data.Count == pageSize)
                            {
                                pageNo++;
                            }
                            //没数据了 退出循环
                            if (items.Data.Count < pageSize && items.Data.Count > 0)
                            {
                                flag = false;
                            }
                        }
                    }
                }
                return listItem;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> UpdateItemQty(BbGood good)
        {
            try
            {
                string method = "beibei.outer.item.qty.update";
                using (HttpClient hc = new HttpClient())
                {
                    Dictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("method", method);
                    dict.Add("app_id", appId);
                    dict.Add("secret", secret);
                    dict.Add("session", session);
                    dict.Add("timestamp", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    dict.Add("version", "1.0");
                    dict.Add("iid", good.Iid);
                    dict.Add("sku_id", good.Sku_Id);
                    dict.Add("outer_id", good.Outer_Id);
                    dict.Add("qty", good.Qty);
                    string sing = CommonHelper.BbOpera(dict, secret);
                    dict.Add("sign", sing);
                    FormUrlEncodedContent content = new FormUrlEncodedContent(dict);
                    HttpResponseMessage msg = await hc.PostAsync(gateway, content);
                    string res = await msg.Content.ReadAsStringAsync();
                    var resQty = CommonHelper.DeJson<BbQtyUpdateResponse>(res);
                    if (resQty != null)
                    {
                        log.Debug("id" + resQty.Data.Iid + "qty" + resQty.Data.Sale_Qty+"    "+ resQty.Success);
                        return resQty.Success;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return false;
        }

    }
}
