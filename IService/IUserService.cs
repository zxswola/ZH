namespace IService
{
    public interface IUserService
    {
        bool CheckLogin(string userName, string password);
        bool CheckUserNameExists(string userName);
    }
}