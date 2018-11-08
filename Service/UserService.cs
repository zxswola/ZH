using IService;

namespace Service
{
    public class UserService : IUserService
    {
        public bool CheckLogin(string userName, string password)
        {
            return true;
        }

        public bool CheckUserNameExists(string userName)
        {
            return false;
        }
    }
}