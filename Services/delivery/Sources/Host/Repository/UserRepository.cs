using Host.Data;
using Host.Interfaces.Repository;
using Host.Models;
using System.Collections.Generic;
using System.Linq;

namespace Host.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DeliveryDbContext _context;

        public UserRepository(DeliveryDbContext context)
        {
            _context = context;
        }

        // GET 
        public ICollection<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public User GetUserById(int userId)
        {
            return _context.Users.FirstOrDefault(u => u.UserId == userId);
        }

        // CREATE 
        public bool CreateUser(User user)
        {
            _context.Add(user);
            return Save();
        }

        // UPDATE
        public bool UpdateUser(User user)
        {
            _context.Update(user);
            return Save();
        }

        // DELETE 
        public bool DeleteUser(User user)
        {
            _context.Remove(user);
            return Save();
        }

        // CHECK
        public bool UserExists(int userId)
        {
            return _context.Users.Any(u => u.UserId == userId);
        }

        // SAVE
        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
