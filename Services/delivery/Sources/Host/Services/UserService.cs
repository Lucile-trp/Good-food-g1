using Host.Interfaces.Repository;
using Host.Interfaces.Services;
using Host.Models;
using System.Collections.Generic;

namespace Host.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET
        public ICollection<User> GetUseres()
        {
            return _userRepository.GetUseres();
        }

        public User GetUserById(int userId)
        {
            return _userRepository.GetUserById(userId);
        }

        // CREATE
        public bool CreateUser(User user)
        {
            return _userRepository.CreateUser(user);
        }

        // UPDATE 
        public bool UpdateUser(User user)
        {
            return _userRepository.UpdateUser(user);
        }

        // DELETE 
        public bool DeleteUser(int userId)
        {
            var user = _userRepository.GetUserById(userId);
            if (user == null)
            {
                return false;
            }
            return _userRepository.DeleteUser(user);
        }

        // CHECK
        public bool UserExists(int userId)
        {
            return _userRepository.UserExists(userId);
        }
    }
}
