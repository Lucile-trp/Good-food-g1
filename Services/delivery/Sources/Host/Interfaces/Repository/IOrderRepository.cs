using Host.Models;
using System.Collections.Generic;

namespace Host.Interfaces.Repository
{
    public interface IOrderRepository
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
        bool DeleteOrder(Order order);
        bool OrderExists(int orderId);
        bool Save();
    }
}
