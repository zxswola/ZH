using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperService.Entities
{
    public class LogEntity
    {
        public int Id { get; set; }
        public string MessageId { get; set; }
        public string Contents { get; set; }
        public DateTime UpdateTime { get; set; } = DateTime.Now;
        public int IsDeleted { get; set; } = 0;
    }
}
