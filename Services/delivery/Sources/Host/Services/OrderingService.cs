using Host.Interfaces.Repository;
using Host.Interfaces.Services;
using Host.Models;

namespace Host.Services
{
    public class OrderingService : IOrderingService
    {
        private readonly IOrderingRepository _orderingRepository;

        public OrderingService(IOrderingRepository repo)
        {
            _orderingRepository = repo;
        }

        public ICollection<Ordering> GetOrderings()
        {
            return _orderingRepository.GetOrderings();
        }

        public ICollection<Ordering> GetOrderingByDishId(int dishId)
        {
            return _orderingRepository.GetOrderingByDishId(dishId);
        }

        public ICollection<Ordering> GetOrderingByOrderId(int orderId)
        {
            return _orderingRepository.GetOrderingByOrderId(orderId);
        }

        public bool CreateOrdering(Ordering ordering)
        {
            return _orderingRepository.CreateOrdering(ordering);
        }

        public bool UpdateOrdering(Ordering ordering)
        {
            return _orderingRepository.UpdateOrdering(ordering);
        }

        public bool DeleteOrderingByDishId(int dishId)
        {
            ICollection<Ordering> orderings = _orderingRepository.GetOrderingByDishId(dishId);
            if (orderings.Any())
            {
                return false;
            }

            bool deleted = false;
            foreach (Ordering ordering in orderings)
            {
                deleted = _orderingRepository.DeleteOrdering(ordering);
            }

            return deleted;
        }

        public bool DeleteOrderingByOrderId(int orderId)
        {
            ICollection<Ordering> orderings = _orderingRepository.GetOrderingByOrderId(orderId);
            if (orderings.Any())
            {
                return false;
            }

            bool deleted = false;
            foreach (Ordering ordering in orderings)
            {
                deleted = _orderingRepository.DeleteOrdering(ordering);
            }
            
            return deleted;
        }

        public bool OrderingExistsByDishId(int dishId)
        {
            return _orderingRepository.OrderingExistsByDishId(dishId);
        }

        public bool OrderingExistsByOrderId(int orderId)
        {
            return _orderingRepository.OrderingExistsByOrderId(orderId);
        }
    }
}