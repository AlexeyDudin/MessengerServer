using Domain.Models;
using MessengerServer.Models;

namespace MessengerServer.Converters
{
    public static class RoleConverter
    {
        public static Role ToRole(this RoleDto roleDto, List<Role> roles)
        {
            Role? parentInDb = null;
            if (!string.IsNullOrEmpty(roleDto.Parent))
            {
                parentInDb = roles.FirstOrDefault(r => r.Name == roleDto.Parent);
            }
            return new Role()
            {
                Name = roleDto.Name,
                Parent = parentInDb
            };
        }

        public static RoleDto ToRoleDto(this Role role)
        {
            var users = new List<string>();
            foreach (var user in role.Users)
            {
                users.Add(user.Login);
            }
            var parent = "";
            if (role.Parent != null)
            {
                parent = role.Parent.Name;
            }
            var childs = new List<RoleDto>();
            foreach (var child in role.Children)
            {
                childs.Add(child.ToRoleDto());
            }
            return new RoleDto()
            {
                Name = role.Name,
                Users = users,
                Parent = parent,
                Child = childs
            };
        }

        public static List<RoleDto> ToRoleDtoList(this List<Role> roles)
        {
            var result = new List<RoleDto>();
            foreach(var role in roles) 
            {
                result.Add(role.ToRoleDto());
            }
            return result;
        }
    }
}
