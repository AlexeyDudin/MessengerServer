using Application.RoleServices;
using Application.UserServices.UserService;
using MessengerServer.Converters;
using MessengerServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MessengerServer.Controllers
{
    [Route("api/role")]
    public class RoleController : Controller
    {
        public readonly IRoleService roleService;
        public RoleController(IRoleService roleService)
        {
            this.roleService = roleService;
        }

        [Authorize]
        [HttpPost, Route("add")]
        public IResult AddRole(RoleDto role)
        {
            return Results.Ok(roleService.AddRole(role.ToRole()));
        }

        [Authorize]
        [HttpGet]
        public IResult GetRoles()
        {
            return Results.Ok(roleService.GetRoles().ToRoleDtoList());
        }
    }
}
