using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
   public  class YzShipmentsResponse
    {
        public SResponse response { get; set; }
    }
    public class SResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public List<StructurizationTrade> delivery_order { get; set; }

    }
    public class StructurizationTrade
    {
        public string pk_id { get; set; }

        /// <summary>
        /// 物流状态 0:待发货; 1:已发货
        /// </summary>
        public int express_state { get; set; }
        public string express_type { get; set; }

    }
}
