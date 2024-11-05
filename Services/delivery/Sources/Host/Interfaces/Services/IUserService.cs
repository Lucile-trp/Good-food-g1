using Host.Models;
using System.Collections.Generic;

namespace Host.Interfaces.Services
{
    public interface IUserService
    {
        ICollection<User> GetUsers();
        User GetUserById(int userId);
        bool CreateUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(int userId);
        bool UserExists(int userId);
    }
}
