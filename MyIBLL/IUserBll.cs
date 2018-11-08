namespace MyIBLL
{
    public interface IUserBll
    {
        bool Check(string username, string pwd);
        void AddNew(string username, string pwd);
    }
}