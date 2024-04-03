namespace Domain.Models
{
    public class Message
    {
        public int Id { get; set; }
        public User From { get; set; }
        public User ToUser { get; set; }
        public Role ToGroup { get; set; }
    }
}
