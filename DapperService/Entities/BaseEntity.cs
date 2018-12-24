using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperService.Entities
{
    public  class BaseEntity
    {
        public int IsDeleted { get; set; } = 0;
        public DateTime CreateDateTIme { get; set; } = DateTime.Now;
    }
}
