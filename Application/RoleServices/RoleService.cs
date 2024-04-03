using Domain.Foundation;
using Domain.Models;

namespace Application.RoleServices
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork unitOfWork;
        public RoleService(IUnitOfWork unitOfWork) 
        {
            this.unitOfWork = unitOfWork;
        }
        public Role AddRole(Role role)
        {
            unitOfWork.RoleRepository.Add(role);
            unitOfWork.Commit();
            return role;
        }

        public Role DeleteRole(Role role)
        {
            unitOfWork.RoleRepository.Delete(role);
            unitOfWork.Commit();
            return role;
        }

        public List<Role> GetRoles()
        {
            return unitOfWork.RoleRepository.GetAll();
        }

        public Role UpdateRole(Role role)
        {
            var roleInDb = unitOfWork.RoleRepository.Where(r => r.Name == role.Name).FirstOrDefault();
            if (roleInDb == null)
            {
                throw new Exception("Ошибка поиска роли в БД");
            }
            roleInDb.Name = role.Name;
            roleInDb.Users = role.Users;
            unitOfWork.Commit();
            return roleInDb;
        }
    }
}
