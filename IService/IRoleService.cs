using DTO;

namespace IService
{
    public interface IRoleService:IServiceSupport
    {
        int AddNew(string roleName);
        void Update(int roleId, string roleName);
        void MarkDeleted(int id);
        RoleDTO GetById(int id);
        RoleDTO GetByName(string name);
        RoleDTO[] GetAll();

        /// <summary>
        /// 给 adminUser增加权限
        /// </summary>
        /// <param name="adminUserId"></param>
        /// <param name="roleIds"></param>
        void AddRoles(int adminUserId, int[] roleIds);
        //更新权限,先删除再添加
        void UpdateRoleIds(int adminUserId, int[] roleIds);
        //获取用户角色
        RoleDTO[] GetByAdminUserId(int adminUserId);
    }
}