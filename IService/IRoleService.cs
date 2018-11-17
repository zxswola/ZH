using DTO;

namespace IService
{
    public interface IRoleService:IServiceSupport
    {
        long AddNew(string roleName);
        void Update(long roleId, string roleName);
        void MarkDeleted(long id);
        RoleDTO GetById(long id);
        RoleDTO GetByName(string name);
        RoleDTO[] GetAll();

        /// <summary>
        /// 给 adminUser增加权限
        /// </summary>
        /// <param name="adminUserId"></param>
        /// <param name="roleIds"></param>
        void AddRoles(long adminUserId, long[] roleIds);
        //更新权限,先删除再添加
        void UpdateRoleIds(long adminUserId, long[] roleIds);
        //获取用户角色
        RoleDTO[] GetByAdminUserId(long adminUserId);
    }
}