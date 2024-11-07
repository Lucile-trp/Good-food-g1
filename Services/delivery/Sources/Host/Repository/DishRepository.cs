using Host.Data;
using Host.Models;

namespace Host.Repository
{
    public class DishRepository : IDishRepository
    {
        private readonly DeliveryDbContext _context;

        public DishRepository(DeliveryDbContext context)
        {
            _context = context;
        }
        public ICollection<Dish> GetDishes()
        {
            return _context.Dishes.ToList();
        }

        public Dish GetDishById(int dishId)
        {
            return _context.Dishes.FirstOrDefault(d => d.Id == dishId);
        }

        public bool DeleteDish(Dish dish)
        {
             _context.Remove(dish);
            return Save();
        }

        public bool DishExists(int dishId)
        {
            return _context.Dishes.Any(da => da.Id == dishId);
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}