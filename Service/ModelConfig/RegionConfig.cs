using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Entities;

namespace Service.ModelConfig
{
    public class RegionConfig:EntityTypeConfiguration<RegionEntity>
    {
        public RegionConfig()
        {
            ToTable("T_Regions");
            Property(e => e.Name).IsRequired().HasMaxLength(200);
            HasRequired(e => e.City).WithMany().HasForeignKey(e => e.CityId).WillCascadeOnDelete(false);
        }
    }
}
