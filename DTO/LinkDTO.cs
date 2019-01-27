using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    [Serializable]
   public  class LinkDTO
    {
        public string Id { get; set; }
        public string Link { get; set; }
        public string ImageName { get; set; }
        public string Name { get; set; }
    }
}
