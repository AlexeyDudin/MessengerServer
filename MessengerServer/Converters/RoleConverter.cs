using Domain.Models;
using MessengerServer.Models;

namespace MessengerServer.Converters
{
    public static class RoleConverter
    {
        public static Role ToRole(this RoleDto roleDto)
        {
            return new Role()
            {
                Name = roleDto.Name,
            };
        }

        public static RoleDto ToRoleDto(this Role role)
        {
            var users = new List<string>();
            foreach (var user in role.Users)
            {
                users.Add(user.Login);
            }
            return new RoleDto()
            {
                Name = role.Name,
                Users = users
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
