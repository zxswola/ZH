namespace DTO
{
    public class CommunityDTO:BaseDTO
    {
        public string Name { get; set; }
        public long RegionId { get; set; }
        public string Loaction { get; set; }
        public string Traffic { get; set; }
        public int? BulitYear { get; set; }
    }
}