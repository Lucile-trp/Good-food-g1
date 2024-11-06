using Host.Models;

namespace Host.Repository
{
    public interface IDishRepository
    {
        ICollection<Dish> GetDishes();
        Dish GetDishById(int dishId);
        bool DeleteDish(Dish dish);
        bool DishExists(int dishId);
        bool Save();
    }
}