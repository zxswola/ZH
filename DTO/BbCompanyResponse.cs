using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class BbCompanyResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public BbCompany[] Data { get; set; }
    }

    public class BbCompany
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Phone { get; set; }
    }
}
