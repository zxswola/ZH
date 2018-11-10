using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Entities;

namespace Service.ModelConfig
{
    public class HousePicConfig:EntityTypeConfiguration<HousePicEntity>
    {
        public HousePicConfig()
        {
            ToTable("T_HousePics");
            HasRequired(e => e.House).WithMany(e => e.HousePics).HasForeignKey(e => e.HouseId)
                .WillCascadeOnDelete(false);
            Property(e => e.Url).IsRequired().HasMaxLength(1024);
            Property(e => e.ThumbUrl).IsRequired().HasMaxLength(1024);

        }
    }
}
