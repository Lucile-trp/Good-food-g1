using Host.Interfaces.Repository;
using Host.Interfaces.Services;
using Host.Models;
using System.Collections.Generic;

namespace Host.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        // GET
        public ICollection<Order> GetOrders()
        {
            return _orderRepository.GetOrders();
        }

        public Order GetOrderById(int orderId)
        {
            return _orderRepository.GetOrderById(orderId);
        }

        // CREATE
        public bool CreateOrder(Order order)
        {
            return _orderRepository.CreateOrder(order);
        }

        // UPDATE 
        public bool UpdateOrder(Order order)
        {
            return _orderRepository.UpdateOrder(order);
        }

        // DELETE 
        public bool DeleteOrder(int orderId)
        {
            var order = _orderRepository.GetOrderById(orderId);
            if (order == null)
            {
                return false;
            }
            return _orderRepository.DeleteOrder(order);
        }

        // CHECK
        public bool OrderExists(int orderId)
        {
            return _orderRepository.OrderExists(orderId);
        }
    }
}
