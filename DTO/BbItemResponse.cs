using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class BbItemResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<BbItemDetail> Data { get; set; }
        public int Count { get; set; }
        public int Page_No { get; set; }
        public int Page_Size { get; set; }

    }

    public class BbItemDetail
    {
        public string Iid { get; set; }
        public string Title { get; set; }
        public List<BbItemSku> Sku { get; set; }
    }

    public class BbItemSku
    {
        public int Id { get; set; }
        public string Outer_Id { get; set; }
        public int Num { get; set; }
    }
}
