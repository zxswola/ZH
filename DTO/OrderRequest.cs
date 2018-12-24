using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class OrderRequest
    {
        /// <summary>
        /// 买家id
        /// </summary>
        public int buyer_id { get; set; }
        /// <summary>
        /// 交易创建结束时间
        /// </summary>
        public DateTime end_created { get; set; }
        /// <summary>
        /// 交易状态更新的结束时间
        /// </summary>
        public DateTime end_update { get; set; }
        /// <summary>
        /// 物流类型搜索 同城送订单：LOCAL_DELIVERY 自提订单：SELF_FETCH 快递配送：EXPRESS
        /// </summary>
        public string express_type { get; set; }
        /// <summary>
        /// 粉丝id
        /// </summary>
        public int fans_id { get; set; }
        /// <summary>
        /// 粉丝类型
        /// </summary>
        public int fans_type { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string goods_title { get; set; }
        /// <summary>
        /// 商品id
        /// </summary>
        public string item_id { get; set; }
        /// <summary>
        /// 通用搜索关键字
        /// </summary>
        public string keywords { get; set; }
        /// <summary>
        /// 是否需要返回订单详情url
        /// </summary>
        public bool need_order_url { get; set; }
        /// <summary>
        /// 门店id
        /// </summary>
        public int offline_id { get; set; }
        /// <summary>
        /// 来源
        /// </summary>
        public string order_source { get; set; }
        /// <summary>
        /// 页码
        /// </summary>
        public int page_no { get; set; }
        /// <summary>
        /// 每页条数
        /// </summary>
        public int page_size { get; set; }
        /// <summary>
        /// 收货人昵称
        /// </summary>
        public string receiver_name { get; set; }
        /// <summary>
        /// 收货人手机号
        /// </summary>
        public string receiver_phone { get; set; }
        /// <summary>
        /// 交易创建的开始时间
        /// </summary>
        public DateTime start_created { get; set; }
        /// <summary>
        /// 交易状态更新的开始时间
        /// </summary>
        public DateTime start_update { get; set; }
        /// <summary>
        /// 订单状态，一次只能查询一种状态 
        /// 待付款：WAIT_BUYER_PAY 
        /// 待发货：WAIT_SELLER_SEND_GOODS 
        /// 等待买家确认：WAIT_BUYER_CONFIRM_GOODS 
        /// 订单完成：TRADE_SUCCESS 
        /// 订单关闭：TRADE_CLOSE 
        /// 退款中：TRADE_REFUND
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string tid { get; set; }
        /// <summary>
        /// 订单类型 NORMAL：普通订单 PEERPAY：代付 GIFT：我要送人 FX_CAIGOUDAN：分销采购单 PRESENT：赠品 
        /// WISH：心愿单 QRCODE：二维码订单 QRCODE_3RD：线下收银台订单 FX_MERGED：合并付货款 VERIFIED：1分钱实名认证 
        /// PINJIAN：品鉴 REBATE：返利 FX_QUANYUANDIAN：全员开店 FX_DEPOSIT：保证金 PF：批发 GROUP：拼团 HOTEL：酒店 
        /// TAKE_AWAY：外卖 CATERING_OFFLINE：堂食点餐 CATERING_QRCODE：外卖买单 BEAUTY_APPOINTMENT：美业预约单 
        /// BEAUTY_SERVICE：美业服务单 KNOWLEDGE_PAY：知识付费 GIFT_CARD：礼品卡
        /// </summary>
        public string type { get; set; }
    }
}
