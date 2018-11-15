using DTO;

namespace IService
{
    public interface IPermissionService:IServiceSupport
    {
        PermissionDTO GetById(long id);
        PermissionDTO[] GetAll();
        //根据权限名称 获取权限
        PermissionDTO GetByName(string name);
        //获取角色权限
        PermissionDTO[] GetByRoleId(long roleId);
        //给角色 增加权限项
        void AddPermids(long roleId, long[] permIds);
        //更新role 权限项  :先删除再添加
        void UpdatePermids(long roleId, long[] permIds);

    }
}