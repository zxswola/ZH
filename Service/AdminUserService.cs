using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using DTO;
using IService;
using Service.Entities;

namespace Service
{
    public class AdminUserService : IAdminUserService
    {
        public long AddAdminUser(string name, string phoneNum, string password, string email, long? cityId)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<AdminUserEntity> bs = new BaseService<AdminUserEntity>(ctx);
                bool exists= bs.GetAll().Any(a => a.PhoneNum == phoneNum);
                if (exists)
                {
                    throw new ArgumentException("此电话已经注册"+phoneNum);
                }

                string passwordSalt = CommonHelper.CreateVerifyCode(5);
                string passwordHash = CommonHelper.CalcMD5(passwordSalt + password);
                AdminUserEntity user = new AdminUserEntity
                {
                    Name = name,
                    PhoneNum = phoneNum,
                    PasswordSalt = passwordSalt,
                    PasswordHash = passwordHash,
                    Email = email,
                    CityId = cityId
                };
                ctx.AdminUsers.Add(user);
                ctx.SaveChanges();
                return user.Id;
            }
        }

        public bool CheckLogin(string phoneNum, string password)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<AdminUserEntity> bs = new BaseService<AdminUserEntity>(ctx);
                var user=bs.GetAll().SingleOrDefault(a => a.PhoneNum == phoneNum);
                if (user == null)
                {
                    return false;
                }
                string userHash = CommonHelper.CalcMD5(user.PasswordSalt + password);
                return user.PasswordHash == userHash;
            }
        }

        AdminUserDTO ToDto(AdminUserEntity adminUser)
        {
            AdminUserDTO dto = new AdminUserDTO();
            dto.CityId = adminUser.CityId;
            if (adminUser.City != null)
            {
                dto.CityName = adminUser.City.Name;
            }
            else
            {
                dto.CityName = "总部";
            }

            dto.Id = adminUser.Id;
            dto.CreateDateTime = adminUser.CreateDateTIme;
            dto.Email = adminUser.Email;
            dto.LastLoginErrorDateTime = adminUser.LastLoginErrorDateTime;
            dto.Name = adminUser.Name;
            dto.PhoneNum = adminUser.PhoneNum;
            return dto;
        }

        public AdminUserDTO[] GetAll(long? cityId)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<AdminUserEntity> bs = new BaseService<AdminUserEntity>(ctx);
                return bs.GetAll().Include(a=>a.City).AsNoTracking().Where(a => a.CityId == cityId).ToList().Select(a => ToDto(a)).ToArray();
            }
        }

        public AdminUserDTO[] GetAll()
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<AdminUserEntity> bs = new BaseService<AdminUserEntity>(ctx);
                return bs.GetAll().Include(a => a.City).AsNoTracking().ToList().Select(a => ToDto(a)).ToArray();
            }
        }

        public AdminUserDTO GetById(long id)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<AdminUserEntity> bs = new BaseService<AdminUserEntity>(ctx);
                var user = bs.GetAll().Include(u => u.City).SingleOrDefault(u => u.Id ==id);
                if (user == null)
                {
                    return null;
                }

                return ToDto(user);
            }
        }

        public AdminUserDTO GetByPhoneNum(string phoneNum)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<AdminUserEntity> bs = new BaseService<AdminUserEntity>(ctx);
                var users = bs.GetAll().Include(u => u.City).AsNoTracking().Where(u => u.PhoneNum == phoneNum);
                //if (user == null)
                //{
                //    return null;
                //}

                //return ToDto(user);
                int count = users.Count();
                if (count <= 0)
                {
                    return null;
                }
                else if (count == 1)
                {
                    return ToDto(users.Single());
                }
                else
                {
                    throw new Exception("找到多个手机号为" + phoneNum + "的管理员");
                }
            }
        }

        public bool HasPermission(long adminUserId, string permissionName)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<AdminUserEntity> bs = new BaseService<AdminUserEntity>(ctx);
               var user= bs.GetAll().Include(u => u.Roles).AsNoTracking().SingleOrDefault(u => u.Id == adminUserId);
                if (user == null)
                {
                    throw new ArgumentException("找不到ID=" + adminUserId + "用户");
                }

                return user.Roles.SelectMany(u => u.Permissions).Any(p => p.Name == permissionName);
            }
        }

        public void MarkDeleted(long adminUserId)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<AdminUserEntity> bs = new BaseService<AdminUserEntity>(ctx);
                bs.MarkDeleted(adminUserId);
            }
        }

        public void RecordLoginError(long id)
        {
            throw new NotImplementedException();
        }

        public void ResetLoginError(long id)
        {
            throw new NotImplementedException();
        }

        public AdminUserDTO[] GetPageData(int pageSize, int index)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<AdminUserEntity> bs = new BaseService<AdminUserEntity>(ctx);
                var users = bs.GetAll().Include(u => u.City).OrderBy(u=>u.Id).Skip((index - 1) * pageSize).Take(pageSize);
                return users.ToList().Select(u => ToDto(u)).ToArray();
            }
        }

        public void UpdateAdminUser(long id, string name, string phoneNum, string password, string email, long? cityId)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<AdminUserEntity> bs = new BaseService<AdminUserEntity>(ctx);
                var user=bs.GetById(id);
                if (user == null)
                {
                    throw new ArgumentException("找不到id为" + id + "的管路员");
                }
                user.Name = name;
                user.PhoneNum = phoneNum;
                if (!string.IsNullOrEmpty(password))
                {
                    string passwordSalt = user.PasswordSalt;
                    string passwordHash = CommonHelper.CalcMD5(passwordSalt + password);
                    user.PasswordHash = passwordHash;
                }
                user.Email = email;
                user.CityId = cityId;
                ctx.SaveChanges();
              
            }
        }

        public int AddAdminUser(string name, string userName, string password, string email,string phoneNum)
        {
            throw new NotImplementedException();
        }

        public void UpdateAdminUser(long id, string name, string userName, string password, string email)
        {
            throw new NotImplementedException();
        }

        public AdminUserDTO GetById(int id)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<AdminUserEntity> bs = new BaseService<AdminUserEntity>(ctx);
                var user = bs.GetById(id);
                return ToDto(user);
            }
        }

        public AdminUserDTO GetByUserName(string userName)
        {
            throw new NotImplementedException();
        }

        public void MarkDeleted(int adminUserId)
        {
            throw new NotImplementedException();
        }

        public bool HasPermission(int adminUserId, string permissionName)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                var user=ctx.Set<AdminUserEntity>().Where(e => e.IsDeleted == false).Include(e=>e.Roles).AsNoTracking().
                    SingleOrDefault(e=>e.Id==adminUserId);
                if (user == null)
                {
                    throw new ArgumentException("找不到id=" + adminUserId + "的用户");
                }

               return user.Roles.SelectMany(r => r.Permissions).Any(p => p.Name == permissionName);
            }

        }

        public void RecordLoginError(int id)
        {
            throw new NotImplementedException();
        }

        public void ResetLoginError(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateAdminUser(long id, string name, string userName, string password, string email, string phoneNum)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<AdminUserEntity> bs = new BaseService<AdminUserEntity>(ctx);
                var user = bs.GetById(id);
                if (user == null)
                {
                    throw new ArgumentException("找不到id为" + id + "的管路员");
                }
                user.Name = name;
                user.PhoneNum = phoneNum;
                user.UserName = userName;
                if (!string.IsNullOrEmpty(password))
                {
                    string passwordSalt = user.PasswordSalt;
                    string passwordHash = CommonHelper.CalcMD5(passwordSalt + password);
                    user.PasswordHash = passwordHash;
                }
                user.Email = email;
                ctx.SaveChanges();

            }
        }
    }
}
