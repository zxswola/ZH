using System.Threading.Tasks;
using DTO;

namespace IService
{
    public interface IPermissionService:IServiceSupport
    {
        Task<long> AddPermission(string perName, string descript);
        void UpdatePermission(long id, string perName, string descript);
        void MarkDelete(long id);
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