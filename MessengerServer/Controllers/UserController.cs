using Application.RoleServices;
using Application.UserServices.UserService;
using MessengerServer.Converters;
using MessengerServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MessengerServer.Controllers
{
    [Route("api/user")]
    public class UserController : Controller
    {
        public readonly IUserService userService;
        public readonly IRoleService roleService;
        public UserController(IUserService userService, IRoleService roleService)
        {
            this.userService = userService;
            this.roleService = roleService;
        }

        [Authorize]
        [HttpPost, Route("add")]
        public IResult AddUser(UserDto user)
        {
            return Results.Ok(userService.AddUser(user.ToUser(roleService.GetRoles())));
        }


        [Authorize]
        [HttpDelete, Route("delete")]
        public IResult DeleteUser(UserDto user)
        {
            return Results.Ok(userService.DeleteUser(user.ToUser(roleService.GetRoles())).ToUserDto());
        }

        [Authorize]
        [HttpPost, Route("update")]
        public IResult UpdateUser(UserDto user)
        {
            return Results.Ok(userService.UpdateUser(user.ToUser(roleService.GetRoles())).ToUserDto());
        }

        [HttpGet, Route("get/{login}/{password}")]
        public IResult GetUser(string login, string password)
        {
            var token = userService.AuthorizeUser(login, password);
            if (string.IsNullOrEmpty(token)) 
            {
                return Results.Unauthorized();
            }
            HttpContext.Response.Cookies.Append(Consts.CookieName, token);
            return Results.Ok(token);
        }

        [Authorize]
        [HttpGet, Route("get/{login}")]
        public IResult GetUserInfo(string login)
        {
            var user = userService.GetUser(login);
            if (user == null)
            {
                Results.BadRequest($"Пользователя с логином {login} не существует");
            }
            return Results.Ok(user.ToUserDto());
        }

        [Authorize]
        [HttpGet]
        public IResult GetAll()
        {
            var user = userService.GetAll();
            return Results.Ok(user.ToUsersDto());
        }
    }
}
