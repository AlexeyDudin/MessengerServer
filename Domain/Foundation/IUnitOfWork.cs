using Domain.Models;

namespace Domain.Foundation
{
    public interface IUnitOfWork
    {
        public IRepository<User> UserRepository { get; }
        public IRepository<Role> RoleRepository { get; }

        public void Commit();
    }
}
