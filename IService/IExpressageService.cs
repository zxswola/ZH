using System.Collections.Generic;
using DTO;

namespace IService
{
    //判断是否发了 快递 的 中间表操作
    public interface IExpressageService
    {
        //添加单个
        bool InsertExpressage(string billId, string orderDtList);
        //添加多个
        void InsertExpressageAll(Dictionary<string, string> dic);
        //修改信息
        void UpdateExpressage(List<string> listBillId);
        //是否存在
        bool IsExit(string billId);
        //获取快递公司列表
        List<AllExpress> GetExpress(string token);
        /// <summary>
        /// 回传物流单号
        /// </summary>
        /// <param name="token"></param>
        /// <param name="tid">订单号</param>
        /// <param name="oids">商品明细单号(拆单,分开)</param>
        /// <param name="out_sid">快递单号</param>
        /// <param name="out_stype">物流公司编号</param>
        /// <param name="outer_tid">外部交易编号</param>
        /// <returns></returns>
        bool AddExpress(string token, string tid, string oids, string out_sid, string out_stype, string outer_tid);

    }
}