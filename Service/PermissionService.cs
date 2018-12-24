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
    public class PermissionService : IPermissionService
    {
        public void AddPermids(long roleId, long[] permIds)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<RoleEntity> bs = new BaseService<RoleEntity>(ctx);
                var role = bs.GetAll().Include(r => r.Permissions).SingleOrDefault(r => r.Id == roleId);
                if (role == null)
                {
                    throw new ArgumentException("不存在角色,ID为" + roleId);
                }

                //role.Permissions.Clear();
                BaseService<PermissionEntity> PerBs = new BaseService<PermissionEntity>(ctx);
                var permissions = PerBs.GetAll().Where(p => permIds.Contains(p.Id)).ToList();
                foreach (var per in permissions)
                {
                    role.Permissions.Add(per);
                }

                ctx.SaveChanges();
            }
        }

        PermissionDTO ToDto(PermissionEntity permission)
        {
            PermissionDTO dto = new PermissionDTO();
            dto.CreateDateTime = permission.CreateDateTIme;
            dto.Description = permission.Description;
            dto.Id = permission.Id;
            dto.Name = permission.Name;
            return dto;
        }

        public PermissionDTO[] GetAll()
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<PermissionEntity> bs = new BaseService<PermissionEntity>(ctx);
                return bs.GetAll().AsNoTracking().ToList().Select(p => ToDto(p)).ToArray();
            }
        }

        public PermissionDTO GetById(long id)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<PermissionEntity> bs = new BaseService<PermissionEntity>(ctx);
                var permission = bs.GetAll().SingleOrDefault(p => p.Id == id);
                if (permission == null)
                {
                   
                }

                return ToDto(permission);
            }
        }

        public PermissionDTO GetByName(string name)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<PermissionEntity> bs = new BaseService<PermissionEntity>(ctx);
                var permission = bs.GetAll().SingleOrDefault(p => p.Name==name);
                if (permission == null)
                {
                    throw new ArgumentException("权限不存在,名字为" +name);
                }

                return ToDto(permission);
            }
        }

        public PermissionDTO[] GetByRoleId(long roleId)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<RoleEntity> bs = new BaseService<RoleEntity>(ctx);
               var role= bs.GetAll().Include(r=>r.Permissions).SingleOrDefault(r => r.Id == roleId);
                if (role == null)
                {
                    throw new ArgumentException("不存在角色,ID为"+roleId);
                }

                return role.Permissions.ToList().Select(p => ToDto(p)).ToArray();
            
            }
        }

        public void UpdatePermids(long roleId, long[] permIds)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<RoleEntity> bs = new BaseService<RoleEntity>(ctx);
                var role = bs.GetAll().Include(r => r.Permissions).SingleOrDefault(r => r.Id == roleId);
                if (role == null)
                {
                    throw new ArgumentException("不存在角色,ID为" + roleId);
                }

                role.Permissions.Clear();
                BaseService<PermissionEntity> PerBs = new BaseService<PermissionEntity>(ctx);
                var permissions= PerBs.GetAll().Where(p => permIds.Contains(p.Id));
                role.Permissions = permissions.ToList();
                ctx.SaveChanges();
            }
        }

        public long AddPermission(string perName, string descript)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<PermissionEntity> bs = new BaseService<PermissionEntity>(ctx);
                bool exists= bs.GetAll().Any(p=>p.Name==perName);
                if (exists)
                {
                    throw new ArgumentException("权限已存在"+ perName);
                }

                PermissionEntity permission = new PermissionEntity();
                permission.Name = perName;
                permission.Description = descript;
                ctx.Permissions.Add(permission);
                ctx.SaveChanges();
                return permission.Id;
            }
        }

        public void UpdatePermission(long id, string perName, string descript)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<PermissionEntity> bs = new BaseService<PermissionEntity>(ctx);
                var permission = bs.GetById(id);
                if (permission == null)
                {
                    throw  new  ArgumentException("没有此权限 ID:"+id);
                }

                permission.Name = perName;
                permission.Description = descript;
                ctx.SaveChanges();

            }
        }

        public void MarkDelete(long id)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<PermissionEntity> bs = new BaseService<PermissionEntity>(ctx);
                var permission = bs.GetById(id);
                if (permission == null)
                {
                    throw new ArgumentException("没有此权限 ID:" + id);
                }

                permission.IsDeleted=true;
                ctx.SaveChanges();

            }
        }

        int IPermissionService.AddPermission(string perName, string descript)
        {
            throw new NotImplementedException();
        }

        public void UpdatePermission(int id, string perName, string descript)
        {
            throw new NotImplementedException();
        }

        public void MarkDelete(int id)
        {
            throw new NotImplementedException();
        }

        public PermissionDTO GetById(int id)
        {
            throw new NotImplementedException();
        }

        public PermissionDTO[] GetByRoleId(int roleId)
        {
            throw new NotImplementedException();
        }

        public void AddPermids(int roleId, int[] permIds)
        {
            throw new NotImplementedException();
        }

        public void UpdatePermids(int roleId, int[] permIds)
        {
            throw new NotImplementedException();
        }
    }
}
