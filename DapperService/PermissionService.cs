using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
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
    public class PermissionService : IPermissionService 
    {
        PermissionDTO ToDto(PermissionEntity permission)
        {
            PermissionDTO dto = new PermissionDTO
            {
                CreateDateTime = permission.CreateDateTIme,
                Description = permission.Description,
                Id = permission.PermId,
                Name = permission.Name
            };
            return dto;
        }
        public  void AddPermids(int  roleId, int[] permIds)
        {
            using (var con = new OracleConnection(OracleHelper.connectionString))
            {
                con.Open();
                string sql = "select roleId from  T_ROLE  where isdeleted=0 and  roleId=:roleId";
                var count = con.Query(sql, new { roleId = roleId }).Count();
                if (count == 0)
                {
                    throw new ArgumentException("角色不存在 ID:" + roleId);
                }
                var transaction = con.BeginTransaction();
                string sqlInsert = "insert into T_ROLEPERMISSION(roleid,permid) values(:roleid,:permid)";
                try
                {

                    foreach (var permId in permIds)
                    {
                        con.ExecuteAsync(sqlInsert, new { roleid = roleId, permid = permId }, transaction);
                       
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

        public async Task<long> AddPermission(string perName, string descript)
        {
            using (var con = new OracleConnection(OracleHelper.connectionString))
            {
                con.Open();
                string sql = "select permid from T_PERMISSION t where t.isdeleted=0 and t.name=:name";
                bool exists = con.Query(sql, new {name = perName}).Any();
                if (exists)
                {
                    throw new ArgumentException("权限已存在" + perName);
                }
                PermissionEntity permission = new PermissionEntity();
                permission.Name = perName;
                permission.Description = descript;
                string sqlInsert = "insert into T_PERMISSION(Name,Description,Isdeleted,Createdatetime) values(:Name,:Description,:Isdeleted,:Createdatetime)";
                await con.ExecuteAsync(sqlInsert, permission);
                return permission.PermId;

            }
        }

        public PermissionDTO[] GetAll()
        {
            using (var con = new OracleConnection(OracleHelper.connectionString))
            {
                con.Open();
                string sql = "select * from T_PERMISSION t where t.isdeleted=0 order by permid ";
                var pers=con.Query<PermissionEntity>(sql);
                return pers.ToList().Select(p => ToDto(p)).ToArray();
            }
        }

        public PermissionDTO GetById(int id)
        {
            using (var con = new OracleConnection(OracleHelper.connectionString))
            {
                con.Open();
                string sql = "select * from T_PERMISSION t where t.isdeleted=0 and permId=:permId ";
                var per = con.Query<PermissionEntity>(sql,new { permId =id}).SingleOrDefault();
                if (per == null)
                {
                    throw new ArgumentException("权限不存在,id为" + id);
                }
                return ToDto(per);
            }
        }

        public PermissionDTO GetByName(string name)
        {
            using (var con = new OracleConnection(OracleHelper.connectionString))
            {
                con.Open();
                string sql = "select * from T_PERMISSION t where t.isdeleted=0 and name=:name ";
                var per = con.Query<PermissionEntity>(sql, new { name =name }).SingleOrDefault();
                if (per == null)
                {
                    throw new ArgumentException("权限不存在,名字为" + name);
                }
                return ToDto(per);
            }
        }

        public PermissionDTO[] GetByRoleId(int roleId)
        {
            using (var con = new OracleConnection(OracleHelper.connectionString))
            {
                con.Open();
                var lookup = new Dictionary<int, RoleEntity>();
                string sql =
                    "select * from  T_Role a left join T_ROLEPERMISSION b on a.roleid=b.roleid left join T_Permission c on b.permid=c.permid where a.isdeleted=0 and a.roleId=:roleId";
                var roles = con.Query<RoleEntity, PermissionEntity, RoleEntity>(sql, (role, per) =>
                    {
                        RoleEntity temp;
                        if (!lookup.TryGetValue(role.RoleId, out temp))
                        {
                            temp = role;
                            lookup.Add(role.RoleId, temp);
                        }

                        if (per != null)
                        {
                            temp.Permissions.Add(per);
                        }

                        return role;
                    },
                    new {roleId = roleId}, splitOn : "roleId");
                  var myRole = lookup.Values.ToList().SingleOrDefault();
                if (myRole != null)
                {
                     
                    return myRole.Permissions.ToList().Select(p => ToDto(p)).ToArray();
                }
                return null;
            }
        }

        public void MarkDelete(int id)
        {
            using (var con = new OracleConnection(OracleHelper.connectionString))
            {
                con.Open();
                string sql = "select permid from T_PERMISSION t where t.isdeleted=0 and t.permid=:permid";
                bool exists = con.Query(sql, new { permid = id }).Any();
                if (!exists)
                {
                    throw new ArgumentException("没有此权限" + id);
                }
           
                string sqlupdate = "update T_PERMISSION  t set t.isdeleted=1 where t.permid=:permid ";
                con.Execute(sqlupdate, new { permid = id });
            }
        }

        public  void UpdatePermids(int roleId, int[] permIds)
        {
            using (var con = new OracleConnection(OracleHelper.connectionString))
            {
                con.Open();
                string sql = "select roleId from  T_ROLE  where isdeleted=0 and  roleId=:roleId";
                var count = con.Query(sql, new { roleId = roleId }).Count();
                if (count == 0)
                {
                    throw new ArgumentException("角色不存在 ID:" + roleId);
                }
                var transaction = con.BeginTransaction();
                string sqlDelete = "delete T_ROLEPERMISSION where roleid=:roleid";
                string sqlInsert = "insert into T_ROLEPERMISSION(roleid,permid) values(:roleid,:permid)";
                try
                {
                    con.Execute(sqlDelete, new {roleid = roleId},transaction);
                    foreach (var permId in permIds)
                    {
                        con.ExecuteAsync(sqlInsert, new { roleid = roleId, permid = permId }, transaction);
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

        public void UpdatePermission(int id, string perName, string descript)
        {
            using (var con = new OracleConnection(OracleHelper.connectionString))
            {
                con.Open();
                string sql = "select permid from T_PERMISSION t where t.isdeleted=0 and t.permid=:permid";
                bool exists = con.Query(sql, new { permid = id }).Any();
                if (!exists)
                {
                    throw new ArgumentException("没有此权限" + id);
                }

                string sqlupdate = "update T_PERMISSION  t set t.name=:name,t.description=:description where t.permid=:permid ";
                con.Execute(sqlupdate, new { permid = id,name=perName, description=descript });
            }
        }

        public void UpdatePermission(long id, string perName, string descript)
        {
            throw new NotImplementedException();
        }

        public void MarkDelete(long id)
        {
            throw new NotImplementedException();
        }

        public PermissionDTO GetById(long id)
        {
            throw new NotImplementedException();
        }

        public PermissionDTO[] GetByRoleId(long roleId)
        {
            throw new NotImplementedException();
        }

        public void AddPermids(long roleId, long[] permIds)
        {
            throw new NotImplementedException();
        }

        public void UpdatePermids(long roleId, long[] permIds)
        {
            throw new NotImplementedException();
        }
    }
}
