using Host.Data;
using Host.Interfaces.Repository;
using Host.Models;

namespace Host.Repository
{
    public class OrderingRepository : IOrderingRepository
    {
        private readonly DeliveryDbContext _context;

        public OrderingRepository(DeliveryDbContext context)
        {
            _context = context;
        }

        public ICollection<Ordering> GetOrderings()
        {
            return _context.Orderings.ToList();
        }

        public ICollection<Ordering> GetOrderingByDishId(int dishId)
        {
            return _context.Orderings.Where(da => da.Dish.Id == dishId).ToList();
        }

        public ICollection<Ordering> GetOrderingByOrderId(int orderId)
        {
            return _context.Orderings.Where(da => da.Order.OrderId == orderId).ToList();
        }


        public bool CreateOrdering(Ordering ordering)
        {
            _context.Add(ordering);
            return Save();
        }

        public bool UpdateOrdering(Ordering ordering)
        {
            _context.Update(ordering);
            return Save();
        }

        public bool DeleteOrdering(Ordering ordering)
        {
            _context.Remove(ordering);
            return Save();
        }

        public bool OrderingExistsByDishId(int dishId)
        {
            return _context.Orderings.Any(da => da.Dish.Id == dishId);
        }

        public bool OrderingExistsByOrderId(int orderId)
        {
            return _context.Orderings.Any(da => da.Order.OrderId == orderId);
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}