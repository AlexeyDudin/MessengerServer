using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    [Table("UsersRoles")]
    public class UserRole
    {
        public long UserId { get; set; }
        public long RoleId { get; set; }
    }
}
