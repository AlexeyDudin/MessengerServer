using Domain.Models;

namespace Domain.Foundation
{
    public interface IUnitOfWork
    {
        public IRepository<User> UserRepository { get; }

        public void Commit();
    }
}
