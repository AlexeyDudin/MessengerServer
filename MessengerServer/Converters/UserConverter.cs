using Domain.Models;
using MessengerServer.Models;

namespace MessengerServer.Converters
{
    public static class UserConverter
    {
        public static User ToUser(this UserDto userDto, List<Role> roleList)
        {
            var userRoles = new List<Role>();
            if (userDto.Roles != null)
            {
                foreach (var role in userDto.Roles)
                {
                    var foundedRole = roleList.Where(r => r.Name == role).FirstOrDefault();
                    if (foundedRole != null)
                    {
                        userRoles.Add(foundedRole);
                    }
                }
            }

            return new User()
            {
                Login = userDto.Login,
                Password = userDto.Password,
                FullName = userDto.FullName,
                Roles = userRoles
            };
        }

        public static UserDto ToUserDto(this User user)
        {
            var roles = new List<string>();
            if (user.Roles != null)
            {
                foreach (var role in user.Roles)
                {
                    roles.Add(role.Name);
                }
            }
            return new UserDto()
            {
                FullName = user.FullName,
                Login = user.Login,
                Password = user.Password,
                Roles = roles
            };
        }

        public static List<UserDto> ToUsersDto(this List<User> users)
        {
            var result = new List<UserDto>();
            foreach (var user in users)
            {
                result.Add(user.ToUserDto());
            }
            return result;
        }
    }
}
