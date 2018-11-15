using DTO;

namespace IService
{
    public interface IUserService:IServiceSupport
    {
        long AddNew(string phoneNum, string password);
        UserDTO GetById(long id);
        UserDTO GetByPhoneNum(string phoneNum);
        bool CheckLogin(string phoneNum, string password);
        void UpdatePwd(long userId, string newPassword);
        void SetUserCityId(long userId, long cityId);

    }
}