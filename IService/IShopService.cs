using System.Collections.Generic;
using DTO;

namespace IService
{
    public interface  IShopService
    {
        /// <summary>
        /// 获取TOKEN码
        /// </summary>
        /// <returns></returns>
        string QueryToken();
     
       
        /// <summary>
        /// 获取订单
        /// </summary>
        /// <param name="Request"></param>
        /// <param name="Token"></param>
        /// <returns></returns>
        OrderResponse GetOrder(OrderRequest Request, string Token);
        /// <summary>
        /// 有赞API-是否已发货
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="Tid"></param>
        /// <returns></returns>
        bool GetShipments(string Token, string Tid);
        /// <summary>
        ///  有赞API-分页获取所有商品
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        List<ItemsItem> GetGoods(string Token);
        /// <summary>
        /// 有赞API-同步库存
        /// </summary>
        /// <param name="gd"></param>
        /// <param name="token"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        bool UpdateSrorage(DYGoods gd, string token);
        /// <summary>
        ///  有赞API-获取商品详情
        /// </summary>
        /// <param name="ItemID"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Sku_Root GetSku_Root(int ItemID, string token);
        /// <summary>
        ///  有赞API-发送数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <param name="apiParams"></param>
        /// <param name="files"></param>
        /// <returns></returns>
        string SendRequest(string url, string method, IDictionary<string, string> apiParams,
            List<KeyValuePair<string, string>> files);
        //有赞增加订单
        string AddOrder(Full_order_info_listItem orderItem);
     
        List<string> GetWaitSendOid(string token);

        int GetAllGoodsCount(string token);

    }
}