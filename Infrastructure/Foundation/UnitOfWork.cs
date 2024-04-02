using Domain.Foundation;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Foundation
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private ApplicationContext _dbContext;
        private IRepository<User> _userRepository;

        public IRepository<User> UserRepository { get { return _userRepository; } }

        public UnitOfWork(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
            _userRepository = new Repository<User>(dbContext);
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
