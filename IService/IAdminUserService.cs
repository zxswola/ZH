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

        int AddAdminUser(string name, string userName, string password, string email, string phoneNum);
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
        void UpdateAdminUser(int id, string name, string userName, string password, string email, string phoneNum);

    
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
        AdminUserDTO GetById(int id);
        /// <summary>
        /// 根据用户名获取管理员
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        AdminUserDTO GetByUserName(string userName);
        //用户名和密码是否正确
        bool CheckLogin(string userName, string password);
        //软删除
        void MarkDeleted(int adminUserId);
        //是否有权限
        bool HasPermission(int adminUserId, string permissionName);
        //记录登录错误
        void RecordLoginError(int id);
        //充值登录错误信息
        void ResetLoginError(int id);


        AdminUserDTO[] GetPageData(int pageSize, int index);


    }
}