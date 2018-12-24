using System;

namespace DTO
{
    public class AdminUserDTO:BaseDTO
    {

        public string Name { get; set; }
        public string UserName { get; set; }
        public string PhoneNum { get; set; }
        public string Email { get; set; }
        public long? CityId { get; set; }
        public string CityName { get; set; }
        public int LoginErrorTimes { get; set; }
        public DateTime? LastLoginErrorDateTime { get; set; }


    }
}