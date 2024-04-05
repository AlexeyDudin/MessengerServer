using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Domain.Models
{
    [Table("Users")]
    public class User
    {
        private List<Role> _roles = new();
        private List<Message> _outputMessages = new ();
        private List<Message> _inputMessages = new ();

        [Key]
        public long Id { get; set; }
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public UserState State { get; set; } = UserState.Offline;
        public virtual List<Role> Roles
        {
            get => LazyLoader.Load(this, ref _roles);
            set => _roles = value;
        }

        public User()
        { 
        }

        public User(ILazyLoader lazyLoader)
        {
            LazyLoader = lazyLoader;
        }

        private ILazyLoader LazyLoader { get; set; }
        public virtual List<Message> OutputMessages 
        {
            get => LazyLoader.Load(this, ref _outputMessages);
            set => _outputMessages = value; 
        }
        public virtual List<Message> InputMessages 
        { 
            get => LazyLoader.Load(this, ref _inputMessages); 
            set => _inputMessages = value; 
        }

        public void ChangeFields(User newUser)
        {
            Password = newUser.Password;
            FullName = newUser.FullName;
            //Roles = newUser.Roles;
        }

        public void ChangeRoles(List<Role> roles, List<Role> fullRoles)
        {
            Roles.Clear();
            foreach (var role in roles)
            {
                var roleInDb = fullRoles.Where(r => r.Name == role.Name).FirstOrDefault();
                if (roleInDb != null)
                {
                    Roles.Add(roleInDb);
                }
            }
        }
    }
}
