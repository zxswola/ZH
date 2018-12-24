using System.Collections.Generic;
using DTO;

namespace IService
{
    public interface IOrderService
    {
        // 获取未同步快递的订单列表
        List<OrderModel> QueryOrder(string sellCompanyId, string buyCompanyId,ref List<string> sourceIds);
        //获取明细表的商品编码
        List<string> QueryDTOrder(string billId);
        // 流水号是否存在
        bool QueryOrderIsExit(string sourceOrderId);
        // 修改SourceTypeID
        bool UpdateOrderSourceTypeID(List<string> str);
        string QueryBarcode(string shopItemId);

      
    }
}