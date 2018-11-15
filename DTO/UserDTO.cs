using System;

namespace DTO
{
    public class UserDTO:BaseDTO
    {
        public string PhoneNum { get; set; }
        public int LoginErrorTimes { get; set; }
        public DateTime? LastLoginErrorDateTIme { get; set; }
        public long? CityId { get; set; }
    }
}