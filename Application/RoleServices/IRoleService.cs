using Domain.Models;

namespace Application.RoleServices
{
    public interface IRoleService
    {
        List<Role> GetRoles();
        Role AddRole(Role role);
        Role UpdateRole(Role role);
        Role DeleteRole(Role role);
    }
}
