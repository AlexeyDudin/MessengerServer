using Application.UserServices.UserService;
using Domain.Foundation;
using Domain.Models;
using Infrastructure.Providers;

namespace Application.UserService
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
            var foundedUserInDb = unitOfWork.UserRepository.Where(u => u.UniqueId == user.UniqueId).FirstOrDefault();
            if (foundedUserInDb != null)
            {
                unitOfWork.UserRepository.Delete(foundedUserInDb);
            }
            return user;
        }

        public string AuthorizeUser(string login, string password)
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
            var findUser = unitOfWork.UserRepository.FirstOrDefault(u => u.UniqueId == user.UniqueId);
            if (findUser == null)
            {
                throw new Exception("Пользователь не найден");
            }
            findUser.ChangeFields(user);
            findUser.ChangeRoles(user.Roles, unitOfWork.RoleRepository.GetAll());
            unitOfWork.Commit();
            return findUser;
        }

        public User? GetUser(string login)
        {
            return unitOfWork.UserRepository.Where(u => u.Login == login).FirstOrDefault();
        }

        public List<User> GetAll()
        {
            return unitOfWork.UserRepository.GetAll();
        }

        public User? SetUserState(string login, string connectionId, UserState state)
        {
            var userInDb = unitOfWork.UserRepository.Where(u => u.Login == login).FirstOrDefault();
            if (userInDb != null)
            {
                userInDb.State = state;
                if (state == UserState.Offline)
                    userInDb.ConnectionId = string.Empty;
                else
                {
                    userInDb.ConnectionId = connectionId;
                }
                unitOfWork.Commit();
            }
            return userInDb;
        }

        public List<Role> GetRolesBy(string login)
        {
            var userInDb = unitOfWork.UserRepository.Where(u => u.Login == login).FirstOrDefault();
            if (userInDb == null)
                return new List<Role>();
            return userInDb.Roles;
        }

        public User? GetUserByConnectionId(string connectionId)
        {
            return unitOfWork.UserRepository.Where(u => u.ConnectionId == connectionId).FirstOrDefault();
        }
    }
}
