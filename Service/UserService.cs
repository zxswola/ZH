using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using IService;

namespace Service
{
    public class UserService : IUserService
    {
        public long AddNew(string phoneNum, string password)
        {
            throw new NotImplementedException();
        }

        public bool CheckLogin(string phoneNum, string password)
        {
            throw new NotImplementedException();
        }

        public UserDTO GetById(long id)
        {
            throw new NotImplementedException();
        }

        public UserDTO GetByPhoneNum(string phoneNum)
        {
            throw new NotImplementedException();
        }

        public void SetUserCityId(long userId, long cityId)
        {
            throw new NotImplementedException();
        }

        public void UpdatePwd(long userId, string newPassword)
        {
            throw new NotImplementedException();
        }
    }
}
