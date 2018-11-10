using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Entities;

namespace Service.ModelConfig
{
    public class HouseConfig:EntityTypeConfiguration<HouseEntity>
    {
        public HouseConfig()
        {
            ToTable("T_Houses");
            HasRequired(e => e.Community).WithMany().HasForeignKey(e => e.CommunityId).WillCascadeOnDelete(false);
            HasRequired(e => e.RoomType).WithMany().HasForeignKey(e => e.RoomTypeId).WillCascadeOnDelete(false);
            HasRequired(e => e.Status).WithMany().HasForeignKey(e => e.StatusId).WillCascadeOnDelete(false);
            HasRequired(e => e.DescorateStatus).WithMany().HasForeignKey(e => e.DecorateStatusId).WillCascadeOnDelete(false);
            HasRequired(e => e.Type).WithMany().HasForeignKey(e => e.TypeId).WillCascadeOnDelete(false);
            Property(e => e.Address).IsRequired().HasMaxLength(200);
            Property(e => e.MonthRent).IsRequired();
            Property(e => e.Area).IsRequired();
            Property(e => e.FloorIndex).IsRequired();
            Property(e => e.OwnerName).IsRequired().HasMaxLength(50);
            Property(e => e.OwnerPhoneNum).IsRequired().HasMaxLength(20);


        }
    }
}
