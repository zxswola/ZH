using System;
using MyIBLL;

namespace MyBLL
{
    public class UserBll:IUserBll
    {
        public bool Check(string username, string pwd)
        {
            Console.WriteLine("检查用户");
            return true;
        }

        public void AddNew(string username, string pwd)
        {
            Console.WriteLine("新增用户");
        }
    }
}