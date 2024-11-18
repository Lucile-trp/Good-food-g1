using Host.Data;
using Host.Interfaces.Repository;
using Host.Models;
using Host.Enums;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Host.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DeliveryDbContext _context;

        public OrderRepository(DeliveryDbContext context)
        {
            _context = context;
        }

        // GET 
        public ICollection<Order> GetOrders()
        {
            return _context.Orders.ToList();
        }

        public Order GetOrderByIdAsNoTracking(int orderId)
        {
            return _context.Orders
            .AsNoTracking() 
            .FirstOrDefault(da => da.OrderId == orderId);
        }

        public Order GetOrderById(int orderId)
        {
            return _context.Orders
            .FirstOrDefault(da => da.OrderId == orderId);
        }

         public ICollection<Order> GetOrdersByCustomer(int customerId)
        {
            return _context.Orders
                .Where(o => o.Customer.UserId == customerId)
                .ToList();
        }

        public ICollection<Order> GetOrdersByDeliverer(int delivererId)
        {
            return _context.Orders
                .Where(o => o.Deliverer.UserId == delivererId)
                .ToList();
        }

        public ICollection<Order> GetOrdersByDeliveryAddress(int deliveryAddressId)
        {
            return _context.Orders
                .Where(o => o.DeliveryAddress.DeliveryAddressId == deliveryAddressId)
                .ToList();
        }

        public ICollection<Order> GetOrdersByState(int state)
        {
            return _context.Orders
                .Where(o => o.OrderState == (OrderState)state)
                .ToList();
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
