using Domain.Foundation;
using Domain.Models;

namespace Application
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public User AddUser(User user)
        {
            unitOfWork.UserRepository.Add(user);
            unitOfWork.Commit();
            return user;
        }

        public User DeleteUser(User user)
        {
            unitOfWork.UserRepository.Delete(user);
            return user;
        }

        public User GetUser(string login, string password)
        {
            var findUser = unitOfWork.UserRepository.FirstOrDefault(u => u.Login == login && u.Password == password);
            if (findUser == null) 
            {
                throw new Exception("Пользователь не найден");
            }

            return findUser;
        }

        public User UpdateUser(User user)
        {
            var findUser = unitOfWork.UserRepository.FirstOrDefault(u => u.Login == user.Login);
            if (findUser == null)
            {
                throw new Exception("Пользователь не найден");
            }
            findUser.ChangeFields(user);
            unitOfWork.Commit();
            return findUser;
        }
    }
}
