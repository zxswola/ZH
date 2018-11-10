using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Entities;

namespace Service.ModelConfig
{
    public class UserConfig:EntityTypeConfiguration<UserEntity>
    {
        public UserConfig()
        {
            ToTable("T_Users");
            Property(e => e.PhoneNum).IsRequired().HasMaxLength(20);
            Property(e => e.PasswordHash).IsRequired().HasMaxLength(100);
            Property(e => e.PasswordSalt).IsRequired().HasMaxLength(100);
            HasRequired(e => e.City).WithMany().HasForeignKey(e => e.CityId).WillCascadeOnDelete(false);

        }
    }
}
