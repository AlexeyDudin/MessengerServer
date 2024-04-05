using Application.RoleServices;
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
            try
            {
                return Results.Ok(roleService.AddRole(role.ToRole(roleService.GetRoles())).ToRoleDto());
            }
            catch (Exception ex) 
            {
                return Results.BadRequest(ex);
            }
        }

        [Authorize]
        [HttpGet]
        public IResult GetRoles()
        {
            try
            {
                return Results.Ok(roleService.GetRoleTree().ToRoleDtoList());
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex);
            }
        }

        [Authorize]
        [HttpPost, Route("update")]
        public IResult UpdateRole(RoleDto role)
        {
            try
            {
                return Results.Ok(roleService.UpdateRole(role.ToRole(roleService.GetRoles())).ToRoleDto());
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex);
            }
        }
    }
}
