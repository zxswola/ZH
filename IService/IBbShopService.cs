using System.Collections.Generic;
using System.Threading.Tasks;
using DTO;

namespace IService
{
    public interface IBbShopService
    {
        //获取贝店物流公司
        Task<BbCompany[]> GetExpress();
        //获取贝店订单
        Task<BbOrderResponse> GetOrder(BbOrderRequest request);
        //调用ZH接口发送增加订单
        string AddOrder(BbOrder orderItem);
        //回传贝店物流信息
        Task<BbExpressResponse> AddExpress(BbExpressRequest request);

        Task<List<string>> GetWaitSendOid();
        //获取在售所有商品
        Task<BbItemResponse> GetItemResponse(int pageNo,int pageSize);
        Task<List<BbItemDetail>> GetListItem();
        Task<bool> UpdateItemQty(BbGood good);

    }
}