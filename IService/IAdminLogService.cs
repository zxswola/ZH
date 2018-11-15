namespace IService
{
    public interface IAdminLogService:IServiceSupport
    {
        /// <summary>
        /// 插入一条日志
        /// </summary>
        /// <param name="adminUserId"></param>
        /// <param name="message"></param>
        void AddNew(long adminUserId,string message);
    }
}