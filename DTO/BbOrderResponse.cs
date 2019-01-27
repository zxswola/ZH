using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class BbOrderResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public List<BbOrder> Data {get;set;}
        public int Count { get; set; }
        public List<string> Oids { get; set; }
    }

    public class BbOrder
    {
        public string Oid { get; set; }
        public string Tpe { get; set; }
        public string User { get; set; }
        public string Nick { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Address { get; set; }
        public string Event_id { get; set; }
        public string Item_num { get; set; }
        public int Status { get; set; }
        public float Total_fee { get; set; }
        public float Shipping_fee { get; set; }
        public float Payment { get; set; }
        public string Receiver_name { get; set; }
        public string Receiver_phone { get; set; }
        public string receiver_address { get; set; }
        public string Company { get; set; }
        public string Out_sid { get; set; }
        public string Pay_time { get; set; }
        public string Remark { get; set; }
        public List<BbItem> Item { get; set; }

    }

    public class BbItem
    {
        public string Sku_id { get; set; }
        public int Iid { get; set; }
        public string Outer_id { get; set; }
        public string Goods_num { get; set; }
        public string Title { get; set; }
        public float Price { get; set; }
        public string Num { get; set; }
        public float Total_fee { get; set; }


    }



}
