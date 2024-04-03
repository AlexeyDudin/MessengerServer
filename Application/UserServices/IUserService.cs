﻿using Domain.Models;

namespace Application.UserServices.UserService
{
    public interface IUserService
    {
        User AddUser(User user);
        User UpdateUser(User user);
        User DeleteUser(User user);
        string AuthorizeUser(string login, string password);
        User? GetUser(string login);
        List<User> GetAll();
    }
}