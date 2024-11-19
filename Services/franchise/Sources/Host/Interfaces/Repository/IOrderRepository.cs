using Host.Models;
using System.Collections.Generic;

namespace Host.Interfaces.Repository
{
    public interface IOrderRepository
    {
        ICollection<Order> GetOrders();
        Order GetOrderById(int orderId);
        bool CreateOrder(Order order);
        bool UpdateOrder(Order order);
        bool DeleteOrder(Order order);
        bool OrderExists(int orderId);
        bool Save();
    }
}
