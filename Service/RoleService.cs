using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<AdminUserEntity> bs = new BaseService<AdminUserEntity>(ctx);
                var  adminUser= bs.GetById(adminUserId);
                if (adminUser==null)
                {
                    throw new ArgumentException("用户不存在 ID:" + adminUserId);
                }

                BaseService<RoleEntity> roleBs = new BaseService<RoleEntity>(ctx);
                var roles =roleBs.GetAll().Where(r => roleIds.Contains(r.Id)).ToArray();
                foreach (var r in roles)
                {
                    adminUser.Roles.Add(r);
                }

                ctx.SaveChanges();

            }
        }

        private RoleDTO ToDTO(RoleEntity role)
        {
            RoleDTO dto = new RoleDTO();
            dto.Name = role.Name;
            dto.CreateDateTime = role.CreateDateTIme;
            dto.Id = role.Id;
            return dto;
        }

        public RoleDTO[] GetAll()
        {

            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<RoleEntity> bs = new BaseService<RoleEntity>(ctx);
                return bs.GetAll().ToList().Select(r => ToDTO(r)).ToArray();
            }
        }

        public RoleDTO[] GetByAdminUserId(long adminUserId)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<AdminUserEntity> bs = new BaseService<AdminUserEntity>(ctx);
                var user = bs.GetById(adminUserId);
                if (user== null)
                {
                    throw new ArgumentException("不存在管理员" + adminUserId);
                }

                return user.Roles.ToList().Select(u => ToDTO(u)).ToArray();

            }
        }

        public RoleDTO GetById(long id)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<RoleEntity> bs = new BaseService<RoleEntity>(ctx);
                var role = bs.GetById(id);
                return role == null?null:ToDTO(role); 
            }
        }

        public RoleDTO GetByName(string name)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<RoleEntity> bs = new BaseService<RoleEntity>(ctx);
                var role = bs.GetAll().Where(r=>r.Name==name).SingleOrDefault();
                return role == null ? null : ToDTO(role); ;
            }
        }

        public void MarkDeleted(long id)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<RoleEntity> bs = new BaseService<RoleEntity>(ctx);
                bs.MarkDeleted(id);
            }
        }

        public void Update(long roleId, string roleName)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<RoleEntity> bs = new BaseService<RoleEntity>(ctx);
                bool exists= bs.GetAll().Any(r => r.Id != roleId&&r.Name==roleName);
                if (exists)
                {
                    throw new ArgumentException("已经存在"+roleName);
                }

                //RoleEntity role = new RoleEntity();
                //role.Id = roleId;
                ////ctx.Entry(role).State = EntityState.Unchanged;
                //role.Name = roleName;
                var role = bs.GetById(roleId);
                role.Name = roleName;
                ctx.SaveChanges();
            }
        }

        public void UpdateRoleIds(long adminUserId, long[] roleIds)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<AdminUserEntity> bs = new BaseService<AdminUserEntity>(ctx);
                var adminUser = bs.GetById(adminUserId);
                if (adminUser == null)
                {
                    throw new ArgumentException("用户不存在 ID:" + adminUserId);
                }

                adminUser.Roles.Clear();
                BaseService<RoleEntity> roleBs = new BaseService<RoleEntity>(ctx);
                var roles = roleBs.GetAll().Where(r => roleIds.Contains(r.Id)).ToArray();
                foreach (var r in roles)
                {
                    adminUser.Roles.Add(r);
                }

                ctx.SaveChanges();

            }
        }

       

        public void Update(int roleId, string roleName)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<RoleEntity> bs = new BaseService<RoleEntity>(ctx);
                bool exists = bs.GetAll().Any(r => r.Id != roleId && r.Name == roleName);
                if (exists)
                {
                    throw new ArgumentException("已经存在" + roleName);
                }

                //RoleEntity role = new RoleEntity();
                //role.Id = roleId;
                ////ctx.Entry(role).State = EntityState.Unchanged;
                //role.Name = roleName;
                var role = bs.GetById(roleId);
                role.Name = roleName;
                ctx.SaveChanges();
            }
        }

        public void MarkDeleted(int id)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<RoleEntity> bs = new BaseService<RoleEntity>(ctx);
                bs.MarkDeleted(id);
            }
        }

        public RoleDTO GetById(int id)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<RoleEntity> bs = new BaseService<RoleEntity>(ctx);
                var role = bs.GetById(id);
                return role == null ? null : ToDTO(role);
            }
        }

        public void AddRoles(int adminUserId, int[] roleIds)
        {
            throw new NotImplementedException();
        }

        public void UpdateRoleIds(int adminUserId, int[] roleIds)
        {
            throw new NotImplementedException();
        }

        public RoleDTO[] GetByAdminUserId(int adminUserId)
        {
            throw new NotImplementedException();
        }
    }
}
