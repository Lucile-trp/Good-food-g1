using Host.Models;

namespace Host.Interfaces.Services
{
    public interface IDishService
    {
        ICollection<Dish> GetDish();
        Dish GetDishById(int dishId);
        bool CreateDish(Dish dish);
        bool DeleteDish(int dishId);
        bool DishExists(int dishId);
    }
}
