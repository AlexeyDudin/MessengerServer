using Application;
using Domain.Models;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

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
        public User AddUser(User user)
        {
            return userService.AddUser(user);
        }


        [Authorize]
        [HttpPost, Route("delete")]
        public User DeleteUser(User user)
        {
            return userService.DeleteUser(user);
        }

        [Authorize]
        [HttpPost, Route("update")]
        public User UpdateUser(User user)
        {
            return userService.UpdateUser(user);
        }

        [HttpGet, Route("get/{login}/{password}")]
        public IResult GetUser(string login, string password)
        {
            var user = userService.GetUser(login, password);
            if (user == null) 
            {
                return Results.Unauthorized();
            }

            var claims = new List<Claim> { new Claim(ClaimTypes.Name, JsonSerializer.Serialize(user)) };
            var jwt = new JwtSecurityToken(issuer: AuthOptions.ISSUER,
                                           audience: AuthOptions.AUDIENCE,
                                           claims: claims,
                                           expires: DateTime.UtcNow.Add(TimeSpan.FromHours(10)),
                                           signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var result = new
            {
                accessToken = new JwtSecurityTokenHandler().WriteToken(jwt),
                userObject = user
            };

            return Results.Json(result);
        }
    }

    public class AuthOptions
    {
        public const string ISSUER = "MyAuthServer"; // издатель токена
        public const string AUDIENCE = "MyAuthClient"; // потребитель токена
        const string KEY = "mysupersecret_secretsecretsecretkey!123";   // ключ для шифрации
        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}
