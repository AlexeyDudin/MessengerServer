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
        public DateTime TimeStamp { get; set; }
        public string Text { get; set; } = string.Empty;
        public string UniqueId { get; set; } = string.Empty;

        public void ChangeValues(Message newValues)
        {
            From = newValues.From;
            ToUser = newValues.ToUser;
            ToGroup = newValues.ToGroup;
            Text = newValues.Text;
            TimeStamp = newValues.TimeStamp;
        }
    }
}
