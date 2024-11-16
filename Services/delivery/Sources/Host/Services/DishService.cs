using Host.Interfaces.Services;
using Host.Models;
using Host.Repository;

namespace Host.Services
{
    public class DishService : IDishService
    {
        private readonly IDishRepository _dishRepository;

        public DishService(IDishRepository repo)
        {
            _dishRepository = repo;
        }

        public ICollection<Dish> GetDish()
        {
            return _dishRepository.GetDishes();
        }

        public Dish GetDishById(int dishId)
        {
            return _dishRepository.GetDishById(dishId);
        }

        public bool CreateDish(Dish dish)
        {
            return _dishRepository.CreateDish(dish);
        }

        public bool DeleteDish(int dishId)
        {
            var dish = _dishRepository.GetDishById(dishId);
            if (dish == null)
            {
                return false;
            }

            return _dishRepository.DeleteDish(dish);
        }

        public bool DishExists(int dishId)
        {
            return _dishRepository.DishExists(dishId);

        }
    }
}