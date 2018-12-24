using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Dapper;
using DapperService.Entities;
using DTO;
using IService;
using Oracle.ManagedDataAccess.Client;

namespace DapperService
{

    public class AdminUserService:IAdminUserService
    {
        //private static string connectionString = ConfigurationManager.ConnectionStrings["oracleConStr"].ConnectionString;
        //private OracleConnection con = DbFactory.GetConnection();
        AdminUserDTO ToDto(AdminUserEntity adminUser)
        {
            AdminUserDTO dto = new AdminUserDTO();

            dto.UserName = adminUser.UserName;
            dto.Id = adminUser.UserId;
            dto.CreateDateTime = adminUser.CreateDateTIme;
            dto.PhoneNum = adminUser.PhoneNumber;
            dto.Email = adminUser.Email;
            dto.LastLoginErrorDateTime = adminUser.LastLoginErrorDateTime;
            dto.Name = adminUser.Name;
           // dto.PhoneNum = adminUser.PhoneNum;
            return dto;
        }
        //con
        //private OracleHelper helper = new OracleHelper();
        public AdminUserDTO[] GetAll()
        {
            using (var con = new OracleConnection(OracleHelper.connectionString))
            {
                con.Open();
                string sql = "select * from T_ADMINUSER where isDeleted=0 order by userid";
                var admins = con.Query<AdminUserEntity>(sql);
                return admins.Any()? admins.Select(a => ToDto(a)).ToArray():null;
            }
         


        }

        public long AddAdminUser(string name, string phoneNum, string password, string email, long? cityId)
        {
            throw new NotImplementedException();
        }

        public int AddAdminUser(string name, string userName, string password, string email,string phoneNum)
        {
            
            using (var con = new OracleConnection(OracleHelper.connectionString))
            {
             
                    con.Open();
                    string sql = "select userid from T_ADMINUSER where isDeleted=0 and username=:username";
                    var admin = con.Query(sql, new {username = userName});
                    var count = admin.Count();
                    if (count > 0)
                    {
                        throw new ArgumentException("此用户名已经注册" + userName);
                    }

                    string passwordSalt = CommonHelper.CreateVerifyCode(5);
                    string passwordHash = CommonHelper.CalcMD5(passwordSalt + password);
                    AdminUserEntity user = new AdminUserEntity
                    {
                        Name = name,
                        UserName = userName,
                        PasswordSalt = passwordSalt,
                        PasswordHash = passwordHash,
                        Email = email,
                        PhoneNumber = phoneNum
                    };
                    string sqlInsert =
                        @"insert into T_ADMINUSER(name,username,Passwordsalt,passwordhash,email,phonenumber,loginerrortimes,lastloginerrordatetime,isdeleted,createdatetime)
                    values(:name, :username, :Passwordsalt, :passwordhash, :email, :phonenumber,:loginerrortimes, :lastloginerrordatetime, :isdeleted, :createdatetime)";
                    con.Execute(sqlInsert, user);
                    return con.ExecuteScalar<int>(sql, new {username = userName});
                   

            }

        }

        public void UpdateAdminUser(long id, string name, string phoneNum, string password, string email, long? cityId)
        {
            throw new NotImplementedException();
        }

        public void UpdateAdminUser(int id, string name, string userName, string password, string email, string phoneNum)
        {
            using (var con = new OracleConnection(OracleHelper.connectionString))
            {
                con.Open();
                string sql = "select * from T_ADMINUSER t where t.isdeleted=0 and t.userid=:userid";
                var user = con.Query<AdminUserEntity>(sql, new {userid = id}).SingleOrDefault();
                if (user == null)
                {
                    throw new ArgumentException("找不到id为" + id + "的管路员");
                }

                user.Name = name;
                user.UserName = userName;
                user.PhoneNumber = phoneNum;
                if (!string.IsNullOrEmpty(password))
                {
                    string passwordSalt = user.PasswordSalt;
                    string passwordHash = CommonHelper.CalcMD5(passwordSalt + password);
                    user.PasswordHash = passwordHash;
                }

                user.Email = email;
                string sqlUpdate =
                    "update T_ADMINUSER t set t.name=:name,t.username=:username,t.passwordhash=:passwordhash,t.phonenumber=:phonenumber,t.email=:email where t.userid=:userid";
                con.Execute(sqlUpdate, user);
            }
        }

        public AdminUserDTO GetById(int id)
        {
            using (var con = new OracleConnection(OracleHelper.connectionString))
            {
                con.Open();
                string sql = "select * from T_ADMINUSER t where t.isdeleted=0 and t.userid=:userid";
                var user = con.Query<AdminUserEntity>(sql, new {userid = id}).SingleOrDefault();
                return user == null ? null : ToDto(user);
            }
        }

        public AdminUserDTO GetByUserName(string userName)
        {
            using (var con = new OracleConnection(OracleHelper.connectionString))
            {
                con.Open();
                string sql = "select * from T_ADMINUSER t where t.isdeleted=0 and t.username=:username";
                var users =con.Query<AdminUserEntity>(sql, new {username = userName});
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
                    throw new Exception("找到多个用户名为" + userName + "的管理员");
                }
            }
        }

        public bool CheckLogin(string userName, string password)
        {
            using (var con = new OracleConnection(OracleHelper.connectionString))
            {
                con.Open();
                string sql = "select * from T_ADMINUSER t where t.isdeleted=0 and t.username=:username";
                var user = con.Query<AdminUserEntity>(sql, new {username = userName}).SingleOrDefault();
                if (user == null)
                {
                    return false;
                }

                string userHash = CommonHelper.CalcMD5(user.PasswordSalt + password);
                return user.PasswordHash == userHash;
            }
        }

        public void MarkDeleted(int adminUserId)
        {
            using (var con = new OracleConnection(OracleHelper.connectionString))
            {
                con.Open();
                string sql = "select * from T_ADMINUSER t where t.isdeleted=0 and t.userid=:userid";
                var user = con.Query<AdminUserEntity>(sql, new {userid = adminUserId }).SingleOrDefault();
                if (user == null)
                {
                    throw new ArgumentException("找不到id为" + adminUserId + "的管路员");
                }

                string sqlUpdate = "update T_ADMINUSER t  set t.isdeleted=1 where t.userid=:userid";
                con.Execute(sqlUpdate, new {userid = adminUserId});
            }
        }

        public bool HasPermission(int adminUserId, string permissionName)
        {
            using (var con = new OracleConnection(OracleHelper.connectionString))
            {
                con.Open();
                string sql = @"select e.* from t_Adminuser a left join t_Adminuserrole b on a.userid=b.userid
                                                                                left join t_Role c on b.roleid=c.roleid 
                                                                                left join t_rolepermission d on c.roleid=d.roleid 
                                                                                left join t_permission e on d.permid=e.permid
                                                                                where a.isdeleted=0 and a.userid=:userid and e.name=:name ";
               return  con.Query<PermissionEntity>(sql, new {userid = adminUserId, name = permissionName}).Any();
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

        public AdminUserDTO[] GetPageData(int pageSize, int index)
        {
            using (var con = new OracleConnection(OracleHelper.connectionString))
            {
                con.Open();
                string sql = @"SELECT * FROM(SELECT ROWNUM AS rowno, r.*
                FROM(SELECT * FROM T_ADMINUSER t where t.isdeleted=0
                 ORDER BY t.userid asc) r
                where ROWNUM <= :pageindex  * :pageSize
                ) admin
                WHERE admin.rowno > (:pageindex-1) * :pageSize";
                var admins = con.Query<AdminUserEntity>(sql, new {pageindex = index, pageSize = pageSize});
                return admins.Select(a => ToDto(a)).ToArray();
              
            }
        }

      


        //public List<AdminUserEntity> GetAll()
        //{

        //    using (DbConnection db = new SqlConnection(conStr))
        //    {
        //        var lookup = new Dictionary<long, AdminUserEntity>();
        //        string sql =
        //            @"select * from T_AdminUsers A LEFT JOIN T_AdminUserRoles B ON A.Id = B.AdminUserId LEFT JOIN T_Roles C ON B.RoleId = C.Id LEFT JOIN T_Cities D ON A.CityId=D.Id";
        //        var admins = db.Query<AdminUserEntity, RoleEntity, CityEntity,AdminUserEntity>(sql, (admin, role,city) =>
        //        {
        //            AdminUserEntity temp;
        //            if (!lookup.TryGetValue(admin.Id, out temp))
        //            {
        //                temp = admin;
        //                lookup.Add(admin.Id, temp);
        //            }

        //            if (role != null)
        //            {
        //                temp.Roles.Add(role);
        //            }

        //            if (city != null)
        //            {
        //                temp.City = city;
        //            }


        //            return admin;
        //        });
        //        return lookup.Values.ToList();
        //    }
        //}
    }
}
