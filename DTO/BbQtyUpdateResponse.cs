using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class BbQtyUpdateResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public BbQty Data {get;set;}
    }

    public class BbQty
    {
        public int Sale_Qty { get; set; }
        public string Iid { get; set; }

    }
}
