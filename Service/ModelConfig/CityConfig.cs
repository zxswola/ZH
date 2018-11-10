﻿using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Entities;

namespace Service.ModelConfig
{
   public class CityConfig:EntityTypeConfiguration<CityEntity>
   {
       public CityConfig()
       {
           ToTable("T_Cities");
           Property(e => e.Name).IsRequired().HasMaxLength(200);

       }
   }
}
