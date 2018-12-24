using DTO;

namespace IService
{
    public interface IPermissionService:IServiceSupport
    {
        int AddPermission(string perName, string descript);
        void UpdatePermission(int id, string perName, string descript);
        void MarkDelete(int id);
        PermissionDTO GetById(int id);
        PermissionDTO[] GetAll();
        //根据权限名称 获取权限
        PermissionDTO GetByName(string name);
        //获取角色权限
        PermissionDTO[] GetByRoleId(int roleId);
        //给角色 增加权限项
        void AddPermids(int roleId, int[] permIds);
        //更新role 权限项  :先删除再添加
        void UpdatePermids(int roleId, int[] permIds);
        

    }
}