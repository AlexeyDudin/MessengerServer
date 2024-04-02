using Application;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MessengerServer.Controllers
{
    [Route("api/user")]
    public class UserController : Controller
    {
        public readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [Authorize]
        [HttpPost, Route("add")]
        public IResult AddUser(User user)
        {
            return Results.Ok(userService.AddUser(user));
        }


        [Authorize]
        [HttpDelete, Route("delete")]
        public IResult DeleteUser(User user)
        {
            return Results.Ok(userService.DeleteUser(user));
        }

        [Authorize]
        [HttpPost, Route("update")]
        public IResult UpdateUser(User user)
        {
            return Results.Ok(userService.UpdateUser(user));
        }

        [HttpGet, Route("get/{login}/{password}")]
        public IResult GetUser(string login, string password)
        {
            var user = userService.GetUser(login, password);
            if (string.IsNullOrEmpty(user)) 
            {
                return Results.Unauthorized();
            }
            HttpContext.Response.Cookies.Append(Consts.CookieName, user);
            return Results.Ok(user);
        }
    }
}
