using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Entities;

namespace Service.ModelConfig
{
    public class AdminLogConfig:EntityTypeConfiguration<AdminLogEntity>
    {
        public AdminLogConfig()
        {
            ToTable("T_AdminLogs");
            Property(e => e.Msg).IsRequired();
            HasRequired(e => e.User).WithMany().HasForeignKey(e => e.UserId).WillCascadeOnDelete(false);
        }
    }
}
