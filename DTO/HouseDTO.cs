using System;

namespace DTO
{
    public class HouseDTO:BaseDTO
    {
        public long CommunityId { get; set; }
        public string CommunityName { get; set; }
        public string CommunityLocation { get; set; }
        public string CommunityTraffic { get; set; }
        public int? BuildYear { get; set; }
        public long RegionId { get; set; }
        public string RegionName { get; set; }
        public long CityId { get; set; }
        public string CityName { get; set; }
        public long RoomTypeId { get; set; }
        public string RoomTypeName { get; set; }
        public string Address { get; set; }
        public int MonthRent { get; set; }
        public long StatusId { get; set; }
        public string StatusName { get; set; }
        public double Area { get; set; }
        public long DecorateStatusId { get; set; }
        public string DecorateStatusName { get; set; }
        public int TotalFloorCount { get; set; }
        public int FloorIndex { get; set; }
        public long TypeId { get; set; }
        public string TypeName { get; set; }
        public string Direction { get; set; }
        public DateTime LookableDateTime { get; set; }
        public DateTime CheckInDateTime { get; set; }
        public string OwnerName { get; set; }
        public string OwnerPhoneNum { get; set; }
        public string Descriotion { get; set; }

        public long[] AttachementIds { get; set; }
        public string FristThumbUrl { get; set; }

    }
}