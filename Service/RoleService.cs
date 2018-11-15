using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using IService;
using Service.Entities;

namespace Service
{
    public class RoleService : IRoleService
    {
        public long AddNew(string roleName)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<RoleEntity> bs = new BaseService<RoleEntity>(ctx);
                bool exits= bs.GetAll().Any(r => r.Name == roleName);
                if (exits)
                {
                    throw new ArgumentException("角色名字已经存在" + roleName);
                }

                RoleEntity role = new RoleEntity();
                role.Name = roleName;
                ctx.Roles.Add(role);
                ctx.SaveChanges();
                return role.Id;
            }
        }

        public void AddRoles(long adminUserId, long[] roleIds)
        {
            throw new NotImplementedException();
        }

        public RoleDTO[] GetAll()
        {
            throw new NotImplementedException();
        }

        public RoleDTO[] GetByAdminUserId(long adminUserId)
        {
            throw new NotImplementedException();
        }

        public RoleDTO GetById(long id)
        {
            throw new NotImplementedException();
        }

        public RoleDTO GetByName(long id)
        {
            throw new NotImplementedException();
        }

        public void MarkDeleted(long id)
        {
            throw new NotImplementedException();
        }

        public void Update(long roleId, string roleName)
        {
            throw new NotImplementedException();
        }

        public void UpdateRoleIds(long adminUserId, long[] roleIds)
        {
            throw new NotImplementedException();
        }
    }
}
