using Domain.Foundation;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
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
        private IRepository<Role> _roleRepository;
        private IRepository<Message> _messageRepository;

        public IRepository<User> UserRepository { get => _userRepository; }
        public IRepository<Role> RoleRepository { get => _roleRepository; }
        public IRepository<Message> MessageRepository { get => _messageRepository; }

        public UnitOfWork(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
            var userRepo = new Repository<User>(dbContext);
            userRepo.Entities.Include(u => u.Roles);
            _userRepository = userRepo;
            var roleRepo = new Repository<Role>(dbContext);
            roleRepo.Entities.Include(r => r.Users);
            _roleRepository = roleRepo;
            _messageRepository = new Repository<Message>(dbContext);
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
