using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class OrderResponse
    {
        public Response response { get; set; }
    }

    public class Response
    {
        /// <summary>
        /// 
        /// </summary>
        public List<Full_order_info_listItem> full_order_info_list { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int total_results { get; set; }
    }

    public class Full_order_info_listItem
    {
        /// <summary>
        /// 
        /// </summary>
        public Full_order_info full_order_info { get; set; }
    }

    public class Full_order_info
    {
        /// <summary>
        /// 
        /// </summary>
        public Address_info address_info { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Remark_info remark_info { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Pay_info pay_info { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Buyer_info buyer_info { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<OrdersItem> orders { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Source_info source_info { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Order_info order_info { get; set; }
    }

    public class Order_info
    {
        /// <summary>
        /// 
        /// </summary>
        public string consign_time { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Order_extra order_extra { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string created { get; set; }

        /// <summary>
        /// 待发货
        /// </summary>
        public string status_str { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string expired_time { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string success_time { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string tid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string confirm_time { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string pay_time { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string update_time { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string pay_type_str { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string is_retail_order { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int pay_type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int team_type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int refund_state { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int close_type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int express_type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Order_tags order_tags { get; set; }
    }

    public class Address_info
    {
        /// <summary>
        /// 
        /// </summary>
        public string self_fetch_info { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string delivery_address { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string delivery_postal_code { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string receiver_name { get; set; }

        /// <summary>
        /// 江苏省
        /// </summary>
        public string delivery_province { get; set; }

        /// <summary>
        /// 苏州市
        /// </summary>
        public string delivery_city { get; set; }

        /// <summary>
        /// 常熟市
        /// </summary>
        public string delivery_district { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string address_extra { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string receiver_tel { get; set; }
    }

    public class Remark_info
    {
        /// <summary>
        /// 留言 
        /// </summary>
        public string buyer_message { get; set; }

        /// <summary>
        /// 订单商家备注
        /// </summary>
        public string trade_memo { get; set; }
    }

    public class Pay_info
    {
        /// <summary>
        /// 
        /// </summary>
        public List<string> outer_transactions { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string post_fee { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string total_fee { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string payment { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<string> transaction { get; set; }
    }

    public class Buyer_info
    {
        /// <summary>
        /// 
        /// </summary>
        public string outer_user_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int64 fans_type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int64 buyer_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int64 fans_id { get; set; }

        /// <summary>
        /// 轻描淡写
        /// </summary>
        public string fans_nickname { get; set; }

        public string buyer_phone { get; set; }
    }

    public class OrdersItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string outer_sku_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sku_unique_code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string goods_url { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int item_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string outer_item_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string discount_price { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int item_type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int num { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int sku_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sku_properties_name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string pic_path { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string oid { get; set; }

        /// <summary>
        /// 实物商品（购买时需填写收货地址，测试商品，不发货，不退款）
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string buyer_messages { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string is_present { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string pre_sale_type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string points_price { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string price { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string total_fee { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string alias { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string payment { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string is_pre_sale { get; set; }
    }

    public class Source
    {
        /// <summary>
        /// 
        /// </summary>
        public string platform { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string wx_entrance { get; set; }
    }

    public class Source_info
    {
        /// <summary>
        /// 
        /// </summary>
        public string is_offline_order { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string book_key { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string biz_source { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Source source { get; set; }
    }

    public class Order_extra
    {
        /// <summary>
        /// 
        /// </summary>
        public string is_from_cart { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string is_member { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string is_points_order { get; set; }
    }

    public class Order_tags
    {
        /// <summary>
        /// 
        /// </summary>
        public string is_use_ump { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string is_secured_transactions { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string is_message_notify { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string is_payed { get; set; }
    }
    public class GoodsItem
    {
        public string item_id { get; set; }

        public int qty { get; set; }

        public double price { get; set; }
    }

    public class AddOrderResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public string ERPORDERID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ErrorMessage { get; set; }
    }

    public class RootResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public AddOrderResponse AddOrderResponse { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Code { get; set; } = "0";
        /// <summary>
        /// 
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}
