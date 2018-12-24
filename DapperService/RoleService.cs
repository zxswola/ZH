using System;
using System.Collections.Generic;
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
    public class RoleService : IRoleService
    {

        private RoleDTO ToDTO(RoleEntity role)
        {
            RoleDTO dto = new RoleDTO();
            dto.Name = role.Name;
            dto.CreateDateTime = role.CreateDateTIme;
            dto.Id = role.RoleId;
            return dto;
        }
        public int AddNew(string roleName)
        {
            using (var con = new OracleConnection(OracleHelper.connectionString))
            {
                con.Open();
                string sql = "select roleid from T_ROLE where isdeleted=0 and name=:name";
               var count= con.Query(sql,new{name=roleName}).Count();
                if (count > 0)
                {
                    throw new ArgumentException("角色名字已经存在" + roleName);
                }
                RoleEntity role = new RoleEntity();
                role.Name = roleName;
                string sqlInsert = "insert into t_role(name,isdeleted,createdatetime) values(:name,:isdeleted,:createdatetime)";
                var transaction = con.BeginTransaction();

                try
                {

                    con.Execute(sqlInsert, role,transaction);
                    var id = con.ExecuteScalar<int>(sql, new { name = roleName },transaction);
                    transaction.Commit();
                    return id;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw e;
                }
             
            }
        }

        public async void AddRoles(int adminUserId, int[] roleIds)
        {
            using (var con = new OracleConnection(OracleHelper.connectionString))
            {
                con.Open();
                string sql = "select userId from  T_ADMINUSER  where isdeleted=0 and  userId=:userId";
                var count = con.Query(sql, new {userId = adminUserId}).Count();
                if (count == 0)
                {
                    throw new ArgumentException("用户不存在 ID:" + adminUserId);
                }
                var transaction = con.BeginTransaction();
                string sqlInsert = "insert into T_ADMINUSERROLE(userid,roleid) values(:userid,:roleid)";
                try
                {

                    foreach (var roleId in roleIds)
                    {
                       await con.ExecuteAsync(sqlInsert, new {userid = adminUserId, roleid = roleId}, transaction);
                    }

                    transaction.Commit();
                   
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }

            }
        }

        public RoleDTO[] GetAll()
        {
            using (var con = new OracleConnection(OracleHelper.connectionString))
            {
                con.Open();
                string sql = "select * from T_ROLE where isdeleted=0 order by roleid";
               var roles= con.Query<RoleEntity>(sql);
                return roles.ToList().Select(r => ToDTO(r)).ToArray();
            }
        }

        public RoleDTO[] GetByAdminUserId(int adminUserId)
        {
            using (var con = new OracleConnection(OracleHelper.connectionString))
            {
                string sql = "select c.* from t_adminUser a left join t_adminuserrole b on a.userid=b.userid left join t_role c on b.roleid=c.roleid  where a.userid=:userid";
                var roles = con.Query<RoleEntity>(sql,new {userid = adminUserId});
                return roles.ToList().Select(r => ToDTO(r)).ToArray();
            }
        }

        public RoleDTO GetById(int id)
        {
            using (var con = new OracleConnection(OracleHelper.connectionString))
            {
                con.Open();
                string sql = "select * from T_ROLE t where t.roleid=:roleid";
                var role=con.Query<RoleEntity>(sql, new { roleid = id }).SingleOrDefault();
                return role == null ? null : ToDTO(role);

            }
        }

        public RoleDTO GetByName(string name)
        {
            using (var con = new OracleConnection(OracleHelper.connectionString))
            {
                con.Open();
                string sql = "select * from T_ROLE t where t.name=:name";
                var role = con.Query<RoleEntity>(sql, new { name = name }).SingleOrDefault();
                return role == null ? null : ToDTO(role);

            }
        }

        public void MarkDeleted(int id)
        {
            using (var con = new OracleConnection(OracleHelper.connectionString))
            {
                con.Open();
                string sql = "update T_ROLE t set t.isdeleted=1 where t.roleid=:roleid";
                con.Execute(sql, new { roleid = id });
            }
        }

        public void Update(int roleId, string roleName)
        {
            using (var con = new OracleConnection(OracleHelper.connectionString))
            {
                con.Open();
                string sql = "select roleid from T_ROLE where isdeleted=0 and name=:name and roleId<>:roleId";
                var count = con.Query(sql, new { name = roleName, roleId = roleId }).Count();
                if (count > 0)
                {
                    throw new ArgumentException("角色名字已经存在" + roleName);
                }
                string sqlUpdate = "update T_ROLE t set t.name=:name where t.roleid=:roleid";
                con.Execute(sqlUpdate, new { name = roleName,roleid = roleId });
            }
        }

        public async void UpdateRoleIds(int adminUserId, int[] roleIds)
        {
            using (var con = new OracleConnection(OracleHelper.connectionString))
            {
                con.Open();
                string sql = "select userid from T_ADMINUSER t  where t.isdeleted=0 and t.userid=:userid";
                var count = con.Query(sql, new { userid = adminUserId }).Count();
                if (count== 0)
                {
                    throw new ArgumentException("用户不存在 ID:" + adminUserId);
                }
                var transaction = con.BeginTransaction();
                string sqlDelete = "delete  T_ADMINUSERROLE where userid=:userid";
                string sqlInsert= "insert into T_ADMINUSERROLE(userid,roleid) values(:userid,:roleid)";
                //string sqlUpdate = "update T_ROLE t set t.name=:name where t.roleid=:roledid";
                try
                {
                    con.Execute(sqlDelete, new {userid = adminUserId}, transaction);
                    foreach (var roleId in roleIds)
                    {
                        await con.ExecuteAsync(sqlInsert, new { userid = adminUserId, roleid = roleId }, transaction);
                    }

                    transaction.Commit();

                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
