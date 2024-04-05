namespace MessengerServer.Models
{
    public class MessageDto
    {
        public string Message { get; set; }
        public string From { get; set; }
        public string ToUser { get; set; }
        public string ToGroup { get; set; }
        public DateTime TimeStamp { get; set; }
        public Guid UniqueId { get; set; }
    }
}
