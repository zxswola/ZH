using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Entities;

namespace Service.ModelConfig
{
    public class RoleConfig:EntityTypeConfiguration<RoleEntity>
    {
        public RoleConfig()
        {
            ToTable("T_Roles");
            Property(e => e.Name).IsRequired().HasMaxLength(200);
            HasMany(r => r.Permissions).WithMany(p => p.Roles).Map(m => m.ToTable("T_RolePermissions")
                .MapLeftKey("RoleId").MapRightKey("PermissionId"));


        }
    }
}
