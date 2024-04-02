using Domain.Models;

namespace Application
{
    public interface IUserService
    {
        User AddUser(User user);
        User UpdateUser(User user);
        User DeleteUser(User user);
        string GetUser(string login, string password);
    }
}
