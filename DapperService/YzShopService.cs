using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Common;
using DTO;
using IService;
using Newtonsoft.Json;
using YZOpenSDK;

namespace DapperService
{


    public class YzShopService : IShopService
    {

        private static string client_id = System.Configuration.ConfigurationManager.AppSettings["client_id"];
        private static string client_secret = System.Configuration.ConfigurationManager.AppSettings["client_secret"];
        private static string kdt_id = System.Configuration.ConfigurationManager.AppSettings["kdt_id"];
        private static string TokenFileName = "token.json";
        private static string TokenFilePath = System.AppDomain.CurrentDomain.BaseDirectory + TokenFileName;
        private YzOrderService orderService = new YzOrderService();
        public List<ItemsItem> GetGoods(string token)
        {
            var count = GetAllGoodsCount(token);
            //每页300条数据的话共有几页数据
            var pageAll = (int)Math.Ceiling((double)count / 300);
            List<ItemsItem> listSkusItem = new List<ItemsItem>();
            if (pageAll > 0)
            {
                for (int i = 1; i <= pageAll;i++)
                {
                    Auth auth = new Token(token);
                    YZClient yzClient = new DefaultYZClient(auth);
                    Dictionary<string, object> dict = new System.Collections.Generic.Dictionary<string, object>();
                    dict.Add("page_no", i);
                    dict.Add("page_size", 300);
                    var result = yzClient.Invoke("youzan.item.search", "3.0.0", "POST", dict, null);
                    var goods = CommonHelper.DeJson<GoodsRoot>(result);
                    listSkusItem.AddRange(goods.response.items);
                }
            }
            return listSkusItem;
        }

        public string QueryToken()
        {
            string TokenJson = CommonHelper.GetFile(TokenFilePath);
            if (!string.IsNullOrEmpty(TokenJson))
            {
                TokenResponse Response = JsonConvert.DeserializeObject<TokenResponse>(TokenJson);
                //已经过期
                if (Response.Time <= DateTime.Now)
                {
                    TokenResponse newResponse = GetToken();
                    newResponse.Time= DateTime.Now.AddSeconds(Convert.ToDouble(newResponse.expires_in));
                    CommonHelper.SetFile(TokenFilePath, JsonConvert.SerializeObject(newResponse));
                    return newResponse.access_token;
                }
                else
                {
                    return Response.access_token;
                }
            }
            else
            {
                TokenResponse newResponse = GetToken();
                newResponse.Time = DateTime.Now.AddSeconds(Convert.ToDouble(newResponse.expires_in));
                CommonHelper.SetFile(TokenFilePath, JsonConvert.SerializeObject(newResponse));
                return newResponse.access_token;
            }
        }

        public OrderResponse GetOrder(OrderRequest Request, string Token)
        {
            Auth auth = new Token(Token);
            YZClient yzClient = new DefaultYZClient(auth);
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("start_update", Request.start_created);
            dict.Add("end_update", Request.end_created);
            dict.Add("page_no", Request.page_no);
            dict.Add("page_size", Request.page_size);
            dict.Add("status", Request.status);
            var result = yzClient.Invoke("youzan.trades.sold.get", "4.0.0", "POST", dict, null);
            return JsonConvert.DeserializeObject<OrderResponse>(result);
        }
        //有赞是否发货
        public bool GetShipments(string token, string tid)
        {
            Auth auth = new Token(token);
            YZClient yzClient = new DefaultYZClient(auth);
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("tid", tid);
            var result = yzClient.Invoke("youzan.trade.get", "4.0.0", "GET", dict, null);
            if (!string.IsNullOrEmpty(result))
            {
                YzShipmentsResponse sr = CommonHelper.DeJson<YzShipmentsResponse>(result);
                if (sr.response != null)
                {
                    if (sr.response.delivery_order != null && sr.response.delivery_order.Count > 0)
                    {
                        return sr.response.delivery_order[0].express_state == 1;
                    }
                }
                else
                {
                    return false;
                }
            }

            return false;

        }

        public Sku_Root GetSku_Root(int ItemID, string token)
        {
            try
            {
                Auth auth = new Token(token);
                YZClient yzClient = new DefaultYZClient(auth);
                Dictionary<string, object> dict = new System.Collections.Generic.Dictionary<string, object>();
                dict.Add("item_id", ItemID);
                var result = yzClient.Invoke("youzan.item.get", "3.0.0", "POST", dict, null);
                var model = CommonHelper.DeJson<Sku_Root>(result);
                if (model.response != null)
                {
                    return model;
                }
            }
            catch (Exception e)
            {
                return null;
              
            }
            return null;
        }

        public TokenResponse GetToken()
        {
            IDictionary<string, string> allParams = new Dictionary<string, string>();
            allParams.Add("client_id", client_id);
            allParams.Add("client_secret", client_secret);
            allParams.Add("grant_type", "silent");
            allParams.Add("kdt_id", kdt_id.ToString());
            string result = SendRequest("https://open.youzan.com/oauth/token", "POST", allParams, null);
            return JsonConvert.DeserializeObject<TokenResponse>(result);

        }

     

        public string SendRequest(string url, string method, IDictionary<string, string> apiParams, List<KeyValuePair<string, string>> files)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("User-Agent", "X-YZ-Client 2.0.0 - CSharp");
                var builder = new UriBuilder(url);
                if (method.ToUpper().Equals("GET"))
                {
                    var query = new StringBuilder();
                    foreach (var item in apiParams)
                    {
                        query.AppendFormat("{0}={1}&", item.Key, item.Value);
                    }
                    builder.Query = query.ToString();
                    var reqUrl = builder.ToString();
                    var response = httpClient.GetAsync(reqUrl).Result;
                    //Console.WriteLine(reqUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        return response.Content.ReadAsStringAsync().Result;
                    }
                    throw new YZException("Internal server error, code: " + response.StatusCode);
                }
                else if (method.ToUpper().Equals("POST"))
                {
                    HttpContent form = null;
                    if (files != null)
                    {
                        var myForm = new MultipartFormDataContent();
                        foreach (var item in apiParams)
                        {
                            myForm.Add(new StringContent(item.Value, Encoding.UTF8, "application/x-www-form-urlencoded"), item.Key);
                        }
                        foreach (var file in files)
                        {
                            var content = new StreamContent(new FileStream(file.Value, FileMode.Open));
                            var fileName = file.Value;
                            var idx = fileName.LastIndexOf("/") + 1;
                            myForm.Add(content, file.Key, fileName.Substring(idx, fileName.Length - idx));
                        }
                        form = myForm;
                    }
                    else
                    {
                        form = new FormUrlEncodedContent(apiParams);
                    }
                    var response = httpClient.PostAsync(url, form).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        return response.Content.ReadAsStringAsync().Result;
                    }
                    throw new YZException("Internal server error, code: " + response.StatusCode);
                }
                throw new YZException("Method not supported");
            }
        }

        public bool UpdateSrorage(DYGoods gd, string token)
        {
            try
            {
                Auth auth = new Token(token); // Auth auth = new Sign("app_id", "app_secret"); 
                YZClient yzClient = new DefaultYZClient(auth);
                Dictionary<string, object> dict = new System.Collections.Generic.Dictionary<string, object>();
                dict.Add("item_id", gd.ItemID);
                dict.Add("quantity", gd.Qty);
                dict.Add("type", 0);
                dict.Add("sku_id", gd.ItemSku);
                var result = yzClient.Invoke("youzan.item.quantity.update", "3.0.0", "POST", dict, null);
                var model = CommonHelper.DeJson<UpdateSrorageResponse>(result);
                if (model.response != null)
                {
                    return model.response.is_success;
                }
            }
            catch (Exception e)
            {
                return false;
              
            }
           return false;
        }
        //调用中和接口向ERP系统插入订单
        public string AddOrder(Full_order_info_listItem OrderItem)
        {
            string HttpResponse = string.Empty;
            List<GoodsItem> ListGoodsItem = new List<GoodsItem>();
            SortedDictionary<string, string> sb = new SortedDictionary<string, string>();
            //详细地址
            sb.Add("address", OrderItem.full_order_info.address_info.delivery_address);
            //市
            sb.Add("city", OrderItem.full_order_info.address_info.delivery_city);
            //商品信息
            foreach (var item in OrderItem.full_order_info.orders)
            {
                //取规格ID 
                ListGoodsItem.Add(new GoodsItem { item_id =orderService.QueryBarcode(item.outer_sku_id) , qty = item.num, price = Convert.ToDouble(item.total_fee) });
            }
            string GoodsJson = JsonConvert.SerializeObject(ListGoodsItem);
            sb.Add("items", GoodsJson);
            //买家手机号
            sb.Add("mobile", OrderItem.full_order_info.buyer_info.buyer_phone == null ? OrderItem.full_order_info.address_info.receiver_tel : OrderItem.full_order_info.buyer_info.buyer_phone);
            //创建时间
            sb.Add("order_created", OrderItem.full_order_info.order_info.pay_time);
            //订单号
            sb.Add("order_id", OrderItem.full_order_info.order_info.tid);
            //省
            sb.Add("province", OrderItem.full_order_info.address_info.delivery_province);
            //备注
            sb.Add("remark", OrderItem.full_order_info.remark_info.buyer_message);
            //收货人名称
            sb.Add("recevier", OrderItem.full_order_info.address_info.receiver_name);
            //收货人手机号
            sb.Add("tel", OrderItem.full_order_info.address_info.receiver_tel);
            //区
            sb.Add("town", OrderItem.full_order_info.address_info.delivery_district);
            string ordersJosonStr = HttpHepler.UrlResponseByPost("add_order", sb);
            if (!string.IsNullOrEmpty(ordersJosonStr))
            {
                RootResponse rootModel = JsonConvert.DeserializeObject<RootResponse>(ordersJosonStr);
                if (rootModel.Code == "0")
                {
                    return rootModel.AddOrderResponse.ERPORDERID;
                }
            }
            return null;
        }

        public List<string> GetWaitSendOid(string token)
        {
            List<string> sourceIds = new List<string>();
            bool flag = true;
            int pageNo = 1;
            while (flag)
            {
                OrderRequest or = new OrderRequest()
                {
                    start_created = DateTime.Now.AddMonths(-2),
                    end_created = DateTime.Now,
                    page_no = pageNo,
                    page_size = 100,
                    status = "WAIT_SELLER_SEND_GOODS"
                };
                OrderResponse orders = GetOrder(or, token);
                if (orders != null)
                {
                    if (orders.response.total_results == 0)
                    {
                        break;
                    }

                    if (orders.response.total_results > 0 && orders.response.total_results < 100)
                    {
                        //100条内 循环完退出
                        flag = false;
                        foreach (var order in orders.response.full_order_info_list)
                        {
                            if (order.full_order_info.order_info.status_str == "待发货")
                            {
                                sourceIds.Add(order.full_order_info.order_info.tid);
                            }
                        }
                    }

                    if (orders.response.total_results == 100)
                    {
                        //100条之后还有数据
                        foreach (var order in orders.response.full_order_info_list)
                        {
                            if (order.full_order_info.order_info.status_str == "待发货")
                            {
                                sourceIds.Add(order.full_order_info.order_info.tid);
                            }
                        }
                        pageNo++;
                    }

                }
            }

            return sourceIds;
        }

        public int GetAllGoodsCount(string token)
        {
            Auth auth = new Token(token);
            YZClient yzClient = new DefaultYZClient(auth);
            Dictionary<string, object> dict = new System.Collections.Generic.Dictionary<string, object>();
            dict.Add("page_no", 1);
            dict.Add("page_size", 2);
            var result = yzClient.Invoke("youzan.item.search", "3.0.0", "POST", dict, null);
            var goods = CommonHelper.DeJson<GoodsRoot>(result);
            var count = goods.response.count;
            return count;
        }
    }
}
