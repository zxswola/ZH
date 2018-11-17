using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IService;

namespace Service
{
    public class AdminLogService : IAdminLogService
    {
        public void AddNew(long adminUserId, string message)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                ctx.AdminUserLogs.Add(new Entities.AdminLogEntity {UserId = adminUserId, Msg = message});
                ctx.SaveChanges();
            }
        }
    }
}
