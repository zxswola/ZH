using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Entities;

namespace Service.ModelConfig
{
   public class HouseAppointmentConfig:EntityTypeConfiguration<HouseAppointmentEntity>
   {
       public HouseAppointmentConfig()
       {
           ToTable("T_HouseAppointments"); 
           Property(e=>e.Name).IsRequired().HasMaxLength(50);
           Property(e => e.PhoneNum).IsRequired().HasMaxLength(50);
           HasOptional(e => e.User).WithMany().HasForeignKey(e => e.UserId).WillCascadeOnDelete(false);
           Property(e => e.VisitDate).IsRequired();
           HasRequired(e => e.House).WithMany().HasForeignKey(e => e.HouseId).WillCascadeOnDelete(false);
           Property(e => e.Status).IsRequired().HasMaxLength(50);
           HasOptional(e => e.AdminUser).WithMany().HasForeignKey(e => e.FollowAdminUserId).WillCascadeOnDelete(false);


       }
   }
}
