namespace Domain.Models
{
    public class Message
    {

        public int Id { get; set; }
        public long FromUserId { get; set; }
        public User From { get; set; }
        public long? ToUserId { get; set; }
        public User? ToUser { get; set; }
        public long? ToGroupId { get; set; }
        public Role? ToGroup { get; set; }
    }
}
