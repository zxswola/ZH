using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class youzanItem
    {
    }
    public class ItemsItem
    {
        ///// <summary>
        ///// 
        ///// </summary>
        //public string created_time { get; set; }
        ///// <summary>
        ///// 
        ///// </summary>
        //public string detail_url { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int quantity { get; set; }
        ///// <summary>
        ///// 
        ///// </summary>
        //public int post_fee { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int item_id { get; set; }
        ///// <summary>
        ///// 
        ///// </summary>
        //public int item_type { get; set; }
        ///// <summary>
        ///// 
        ///// </summary>
        //public List<Item_imgsItem> item_imgs { get; set; }
        ///// <summary>
        ///// 
        ///// </summary>
        //public string title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string item_no { get; set; }
        ///// <summary>
        ///// 
        ///// </summary>
        //public string update_time { get; set; }
        ///// <summary>
        ///// 
        ///// </summary>
        //public int price { get; set; }
        ///// <summary>
        ///// 
        ///// </summary>
        //public string alias { get; set; }
        ///// <summary>
        ///// 
        ///// </summary>
        //public int post_type { get; set; }
        ///// <summary>
        ///// 
        ///// </summary>
        //public Delivery_template delivery_template { get; set; }
    }

    public class GoodsResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public int count { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ItemsItem> items { get; set; }
    }

    public class GoodsRoot
    {
        /// <summary>
        /// 
        /// </summary>
        public GoodsResponse response { get; set; }
    }

    public class BasicModel
    {
        /// <summary>
        /// 仓库编码
        /// </summary>
        public string StockCk { get; set; }
        /// <summary>
        /// 比例
        /// </summary>
        public string StockPer { get; set; }

    }

    public class UpdateSrorageResponse
    {
        public SrorageResponse1 response { get; set; }
        //public SrorageResponse2 error_response { get; set; }
    }
    public class SrorageResponse1
    {
        public bool is_success { get; set; }
    }

    /// <summary>
    /// 有赞商品
    /// </summary>
    public class DYGoods
    {
        /// <summary>
        /// 有赞- 商品编码
        /// </summary>
        public string ItemNo { get; set; }
        /// <summary>
        /// 有赞- sku 没配置规格为空
        /// </summary>
        public string ItemSku { get; set; }
        /// <summary>
        /// 有赞- 商品ID
        /// </summary>
        public int ItemID { get; set; }
        /// <summary>
        /// 库存
        /// </summary>
        public int Qty { get; set; }

        /// <summary>
        /// 处理后的商品编码
        /// </summary>
        public string GoodNo { get; set; }

    }

    public class Sku_SkusItem
    {
        ///// <summary>
        ///// 
        ///// </summary>
        //public string sku_unique_code { get; set; }
        ///// <summary>
        ///// 
        ///// </summary>
        //public int with_hold_quantity { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int quantity { get; set; }
        ///// <summary>
        ///// 
        ///// </summary>
        //public int item_id { get; set; }
        ///// <summary>
        ///// 
        ///// </summary>
        //public string created { get; set; }
        ///// <summary>
        ///// 
        ///// </summary>
        //public int price { get; set; }
        ///// <summary>
        ///// [{"vid":348220,"v":"基础黑","kid":16031,"k":"颜色分类"},{"vid":1322,"v":"S/165","kid":12,"k":"尺码"}]
        ///// </summary>
        //public string properties_name_json { get; set; }
        ///// <summary>
        ///// 
        ///// </summary>
        //public string modified { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int sku_id { get; set; }
        ///// <summary>
        ///// 
        ///// </summary>
        //public int sold_num { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string item_no { get; set; }
    }

    public class Sku_Item
    {

        public List<Sku_SkusItem> skus { get; set; }

    }

    public class Sku_Response
    {
        /// <summary>
        /// 
        /// </summary>
        public Sku_Item item { get; set; }
    }

    public class Sku_Root
    {
        /// <summary>
        /// 
        /// </summary>
        public Sku_Response response { get; set; }
    }

    public class OrderModel
    {
        /// <summary>
        /// 流水号
        /// </summary>
        public string BillID { get; set; }
        /// <summary>
        /// 源订单号
        /// </summary>
        public string SourceOrderID { get; set; }
        /// <summary>
        /// 快递平台单号
        /// </summary>
        public string WAYBILLID { get; set; }

        /// <summary>
        /// 快递平台快递类型
        /// </summary>
        public string ExpressWay { get; set; }
        /// <summary>
        /// 快递名称
        /// </summary>
        public string ExpressName { get; set; }
        /// <summary>
        /// 第三方平台快递ID
        /// </summary>
        public int ExpressID { get; set; }
        /// <summary>
        /// 是否拆单 N/Y
        /// </summary>
        public string ISSPLIT { get; set; }
        /// <summary>
        /// 是否同步
        /// </summary>
        //public bool IsTongbu { get; set; } = false;
        /// <summary>
        /// 商品明细信息
        /// </summary>
        public string OrderDTList { get; set; }

        public string ExpressCode { get; set; }
    }

    public class OrderList
    {
        /// <summary>
        /// 订单明细编号
        /// </summary>
        public string oid { get; set; }
        /// <summary>
        /// 商品编号
        /// </summary>
        public string itemid { get; set; }
    }
    public class SrorageModel
    {
        public string StorageID { get; set; }
        public string ItemID { get; set; }
        public string EndQty { get; set; }
    }
}
