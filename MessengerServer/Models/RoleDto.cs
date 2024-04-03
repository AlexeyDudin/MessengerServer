namespace MessengerServer.Models
{
    public class RoleDto
    {
        public string Name { get; set; } = string.Empty;
        public List<string> Users { get; set; } = new List<string>();
    }
}
