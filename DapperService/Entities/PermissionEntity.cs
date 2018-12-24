using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperService.Entities
{
    public class PermissionEntity:BaseEntity
    {
        public int PermId { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public virtual ICollection<RoleEntity> Roles { get; set; } = new List<RoleEntity>();
    }
}
