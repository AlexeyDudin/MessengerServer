﻿using Domain.Models;

namespace Application.RoleServices
{
    public interface IRoleService
    {
        List<Role> GetRoles();
        List<Role> GetRoleTree();
        Role AddRole(Role role);
        Role UpdateRole(Role role);
        Role DeleteRole(Role role);
    }
}
