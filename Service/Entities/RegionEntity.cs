﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Entities
{
   public class RegionEntity:BaseEntity
    {
        public string Name { get; set; }
        public long CityId { get; set; }
        public virtual CityEntity City { get; set; }
    }
}
