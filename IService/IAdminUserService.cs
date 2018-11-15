using DTO;

namespace IService
{
    public interface IAdminUserService:IServiceSupport
    {
        /// <summary>
        /// 新增Admin用户
        /// </summary>
        /// <param name="name"></param>
        /// <param name="phoneNum"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <param name="cityId"></param>
        /// <returns></returns>
        long AddAdminUser(string name, string phoneNum, string password, string email, long? cityId);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="phoneNum"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <param name="cityId"></param>
        void UpdateAdminUser(long id,string name, string phoneNum, string password, string email, long? cityId);
        /// <summary>
        /// 获取cityId这个城市下的管理员
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        AdminUserDTO[] GetAll(long? cityId);
        /// <summary>
        /// 获取所有管理员
        /// </summary>       
        /// <returns></returns>
        AdminUserDTO[] GetAll();
        /// <summary>
        /// 根据id获取管理员
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        AdminUserDTO GetById(long id);
        /// <summary>
        /// 根据电话获取管理员
        /// </summary>
        /// <param name="phoneNum"></param>
        /// <returns></returns>
        AdminUserDTO GetByPhoneNum(string phoneNum);
        //用户名和密码是否正确
        bool CheckLogin(string phoneNum,string password);
        //软删除
        void MarkDeleted(long adminUserId);
        //是否有权限
        bool HasPermission(long adminUser, string permissionName);
        //记录登录错误
        void RecordLoginError(long id);
        //充值登录错误信息
        void ResetLoginError(long id);





    }
}