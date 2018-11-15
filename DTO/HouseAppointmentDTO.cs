using System;

namespace DTO
{
    public class HouseAppointmentDTO:BaseDTO
    {
        public long? UserId { get; set; }
        public string Name { get; set; }
        public string PhoenNum { get; set; }
        public DateTime VisitDate { get; set; }
        public long HouseId { get; set; }
        public string Status { get; set; }
        public long? FollowAdminUserId { get; set; }
        public string FollowAdminUserName { get; set; }
        public DateTime? FollowDateTime { get; set; }
        public string RegionName { get; set; }
        public string CommunityName { get; set; }
        public string HouseAddress { get; set; }

    }
}