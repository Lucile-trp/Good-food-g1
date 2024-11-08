using Host.Models;
using System.Collections.Generic;

namespace Host.Interfaces.Services
{
    public interface IOrderService
    {
        ICollection<Order> GetOrders();
        Order GetOrderByIdAsNoTracking(int orderId);
        Order GetOrderById(int orderId);
        ICollection<Order> GetOrdersByCustomer(int customerId);
        ICollection<Order> GetOrdersByDeliverer(int delivererId);
        ICollection<Order> GetOrdersByDeliveryAddress(int deliveryAddressId);
        ICollection<Order> GetOrdersByState(int state);
        bool CreateOrder(Order order);
        bool UpdateOrder(Order order);
        bool DeleteOrder(int orderId);
        bool OrderExists(int orderId);
    }
}
