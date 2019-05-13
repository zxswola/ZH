using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Service.Entities;

namespace Service
{
   public class MyDbContext:DbContext
   {
       private static ILog log = LogManager.GetLogger(typeof(MyDbContext));
        static MyDbContext()
        {
            Database.SetInitializer<MyDbContext>(null);
        }

        public MyDbContext() : base("name=conStr")
        {
            this.Database.Log = (sql) =>
            {
//                log.Debug(sql); 
                log.DebugFormat("EF执行sql:{0}", sql);

            };
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<AdminLogEntity> AdminUserLogs { get; set; }
        public DbSet<AdminUserEntity> AdminUsers { get; set; }
        public DbSet<AttachmentEntity> Attachements { get; set; }
        public DbSet<CityEntity> Cities { get; set; }
        public DbSet<CommunityEntity> Communities { get; set; }
        public DbSet<HouseAppointmentEntity> HouseAppointments { get; set; }
        public DbSet<HouseEntity> Houses { get; set; }
        public DbSet<HousePicEntity> HousePics { get; set; }
        public DbSet<IdNameEntity> IdNames { get; set; }
        public DbSet<PermissionEntity> Permissions { get; set; }
        public DbSet<RegionEntity> Regions { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<SettingEntity> Settings { get; set; }
        public DbSet<UserEntity> Users { get; set; }
    }
}
