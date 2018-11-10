using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Entities;

namespace Service.ModelConfig
{
    public class AttachmentConfig:EntityTypeConfiguration<AttachmentEntity>
    {
        public AttachmentConfig()
        {
            ToTable("T_Attachments");
            Property(e => e.Name).IsRequired().HasMaxLength(50);
            Property(e => e.IconName).IsRequired().HasMaxLength(50);
            HasMany(e => e.Housers).WithMany(e => e.Attachments).Map(e =>
                e.ToTable("T_HouseAttachments").MapLeftKey("HouseId").MapRightKey("AttchementId"));
        }
    }
}
