using Host.Models;

namespace Host.Interfaces.Services
{
    public interface IOrderingService
    {
        ICollection<Ordering> GetOrderings();
        ICollection<Ordering> GetOrderingByDishId(int dishId);
        ICollection<Ordering> GetOrderingByOrderId(int orderId);
        bool CreateOrdering(Ordering ordering);
        bool UpdateOrdering(Ordering ordering);
        bool DeleteOrderingByDishId(int dishId);
        bool DeleteOrderingByOrderId(int orderId);
        bool OrderingExistsByDishId(int dishId);
        bool OrderingExistsByOrderId(int orderId);
    }
}
