using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Entities;

namespace Service.ModelConfig
{
    public class CommunityConfig:EntityTypeConfiguration<CommunityEntity>
    {
        public CommunityConfig()
        {
            ToTable("T_Communities");
            Property(e => e.Name).IsRequired().HasMaxLength(200);
            HasRequired(e => e.Region).WithMany().HasForeignKey(e => e.RegionId).WillCascadeOnDelete(false);
        }
    }
}
