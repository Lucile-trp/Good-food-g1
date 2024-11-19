using Host.Models;
using System.Collections.Generic;

namespace Host.Interfaces.Services
{
    public interface IOrderService
    {
        ICollection<Order> GetOrders();
        Order GetOrderById(int orderId);
        bool CreateOrder(Order order);
        bool UpdateOrder(Order order);
        bool DeleteOrder(int orderId);
        bool OrderExists(int orderId);
    }
}
