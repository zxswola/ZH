namespace DTO
{
    public class AdminLogDTO:BaseDTO
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string Msg { get; set; }
    }
}