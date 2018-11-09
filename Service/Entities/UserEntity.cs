using System;

namespace Service.Entities
{
    public class UserEntity:BaseEntity
    {
        public string PhoneNum { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public int LoginErrorTimes { get; set; }
        public DateTime? LastLoginErrorDateTime { get; set; }
        public long? CityId { get; set; }
        public virtual CitiyEntity City { get; set; }
    }
}