using Domain.Foundation;
using Domain.Models;
using Infrastructure.Providers;

namespace Application
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IJwtTokenProvider jwtTokenProvider;

        public UserService(IUnitOfWork unitOfWork, IJwtTokenProvider jwtTokenProvider)
        {
            this.unitOfWork = unitOfWork;
            this.jwtTokenProvider = jwtTokenProvider;
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

        public string GetUser(string login, string password)
        {
            var findUser = unitOfWork.UserRepository.FirstOrDefault(u => u.Login == login && u.Password == password);
            if (findUser == null) 
            {
                throw new Exception("Пользователь не найден");
            }
            var token = jwtTokenProvider.GenerateToken(findUser);
            return token;
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
