using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class BbOrderRequest
    {
        public int Status { get; set; }
        public string TimeRange { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }

    }
}
