using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class YzExpress
    {
    }

    public class ExpressResponse
    {
        public ExpResponse response { get; set; }

        public Error_response error_response { get; set; }
    }
    public class ExpResponse
    {
        public bool is_success { get; set; } = false;
    }
    public class Error_response
    {
        public int code { get; set; }
        public string msg { get; set; }
    }

    //
    public class GetExpressResponse
    {
        public GetExpressResponse1 Response { get; set; }
    }

    public class GetExpressResponse1
    {
        public List<AllExpress> allExpress { get; set; }
    }
    public class AllExpress
    {
        public int id { get; set; }
        public string name { get; set; }
        public int display { get; set; }
    }
}
