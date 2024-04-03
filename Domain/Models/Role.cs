using Microsoft.EntityFrameworkCore.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    [Table("Roles")]
    public class Role
    {
        private List<User> _users = new ();
        private ILazyLoader LazyLoader { get; set; }
        public Role() { }

        public Role(ILazyLoader lazyLoader)
        { 
            LazyLoader = lazyLoader;
        }
        [Key]
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public virtual List<User> Users 
        { 
            get => LazyLoader.Load(this, ref _users); 
            set => _users = value;
        }
    }
}
