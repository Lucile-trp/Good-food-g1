using Host.Models;
using System.Collections.Generic;

namespace Host.Interfaces.Repository
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
        User GetUserByIdAsNoTracking(int userId);
        User GetUserById(int userId);
        bool CreateUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(User user);
        bool UserExists(int userId);
        bool Save();
    }
}
