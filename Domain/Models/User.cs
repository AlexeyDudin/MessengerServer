using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public long Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public UserRoles Role { get; set; }

        public void ChangeFields(User newUser)
        {
            Password = newUser.Password;
            FullName = newUser.FullName;
            Role = newUser.Role;
        }
    }
}
