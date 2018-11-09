namespace Service.Entities
{
    public class HousePicEntity:BaseEntity
    {
        public long HouseId { get; set; }
        public HouseEntity House { get; set; }
        public string Url { get; set; }
        public string ThumbUrl { get; set; }

    }
}