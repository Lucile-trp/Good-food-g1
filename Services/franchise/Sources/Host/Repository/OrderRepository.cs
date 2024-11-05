using Host.Data;
using Host.Interfaces.Repository;
using Host.Models;
using System.Collections.Generic;
using System.Linq;

namespace Host.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly FranchiseDbContext _context;

        public OrderRepository(FranchiseDbContext context)
        {
            _context = context;
        }

        // GET 
        public ICollection<Order> GetOrders()
        {
            return _context.Orders.ToList();
        }

        public Order GetOrderById(int orderId)
        {
            return _context.Orders.FirstOrDefault(da => da.OrderId == orderId);
        }

        // CREATE 
        public bool CreateOrder(Order order)
        {
            _context.Add(order);
            return Save();
        }

        // UPDATE
        public bool UpdateOrder(Order order)
        {
            _context.Update(order);
            return Save();
        }

        // DELETE 
        public bool DeleteOrder(Order order)
        {
            _context.Remove(order);
            return Save();
        }

        // CHECK
        public bool OrderExists(int orderId)
        {
            return _context.Orders.Any(da => da.OrderId == orderId);
        }

        // SAVE
        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
