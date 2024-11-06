using Host.Models;

namespace Host.Interfaces.Repository
{
    public interface IOrderingRepository
    {
        ICollection<Ordering> GetOrderings();
        ICollection<Ordering> GetOrderingByDishId(int dishId);
        ICollection<Ordering> GetOrderingByOrderId(int orderId);
        bool CreateOrdering(Ordering ordering);
        bool UpdateOrdering(Ordering ordering);
        bool DeleteOrdering(Ordering ordering);
        bool OrderingExistsByDishId(int dishId);
        bool OrderingExistsByOrderId(int orderId);
        bool Save();
    }
}
