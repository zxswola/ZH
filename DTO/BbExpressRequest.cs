using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class BbExpressRequest
    {
        //订单号
        public string Oid { get; set; }
        //快递公司代码
        public string Company { get; set; }
        //快递单号
        public string Out_sid { get; set; }
        //包裹下的商品信息，一个包裹可能包含多个商品，所以以json数组的格式传入
        //如：[{"outer_id": "7865946","num": 1},{"outer_id": "709609767","num": 2}]。
        public string Order_items { get; set; }
    }
}
