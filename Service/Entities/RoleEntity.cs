using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Entities
{
    public class RoleEntity:BaseEntity
    {
        public int RoleId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<PermissionEntity> Permissions { get; set; } = new List<PermissionEntity>();
        public virtual ICollection<AdminUserEntity> AdminUsers { get; set; } = new List<AdminUserEntity>();
    }
}
