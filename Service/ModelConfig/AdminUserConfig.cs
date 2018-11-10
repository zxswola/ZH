using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Entities;

namespace Service.ModelConfig
{
    public class AdminUserConfig:EntityTypeConfiguration<AdminUserEntity>
    {
        public AdminUserConfig()
        {
            ToTable("T_AdminUsers");
            Property(e => e.Name).IsRequired().HasMaxLength(50);
            Property(e => e.PhoneNum).IsRequired().HasMaxLength(50);
            Property(e => e.PasswordSalt).IsRequired().HasMaxLength(50);
            Property(e => e.PasswordHash).IsRequired().HasMaxLength(250);
            HasOptional(e => e.City).WithMany().HasForeignKey(e => e.CityId).WillCascadeOnDelete(false);
            HasMany(e => e.Roles).WithMany(r => r.AdminUsers).Map(m =>
                m.ToTable("T_AdminUserRoles").MapLeftKey("AdminUserId").MapRightKey("RoleId"));
        }
    }
}
