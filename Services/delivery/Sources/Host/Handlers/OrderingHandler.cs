using AutoMapper;
using Host.Dto;
using Host.Dto.Rpc;
using Host.Enums;
using Host.Interfaces.Services;
using Host.Models;
using Host.RabbitMQ.Handler;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using RabbitMQ.Connection;
namespace Host.Handlers
{
    public class OrderingHandler : RabbitMQMessageHandler
    {
        private readonly ILogger Logger;
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        private readonly IDishService _dishService;
        private readonly IDeliveryAddressService _deliveryAddressService; 
        private readonly IMapper _mapper;
        
        public OrderingHandler(IRabbitMQPersistentConnection persistentConnection, 
            ILoggerFactory loggerFactory,
            IOrderService orderService, 
            IUserService userService, 
            IDishService dishService, 
            IDeliveryAddressService deliveryAddressService,
            IMapper mapper
        ) : base(persistentConnection)
        {
            _orderService = orderService;
            _userService = userService;
            _dishService = dishService;
            _deliveryAddressService = deliveryAddressService;
            _mapper = mapper;

            Logger = loggerFactory.CreateLogger<OrderingHandler>();
        }

        public override void HandleMessage(string content)
        {
            var orderRecv = JsonConvert.DeserializeObject<OrderRecvDto>(content);
            Logger.LogInformation("Received: {0}", content);

            if (orderRecv.Dishes.Length == 0)
            {
                Logger.LogInformation("Dishes not found.");
                return;
            }

            var customer = _userService.GetUserById(orderRecv.CustomerId);

            var deliverer = _userService.GetUserById(orderRecv.DeliveryId);

            // Vérification de l'existence de l'adresse de livraison
            var deliveryAddress = _deliveryAddressService.GetDeliveryAddressById(orderRecv.DeliveryAdresseId);
            if (deliveryAddress == null)
            {
                Logger.LogInformation("Delivery address not found.");
                return;
            }

            // Si l'adresse de livraison existe, on peut l'utiliser pour la commande
            Order orderEntity = new() {
                Customer = customer ?? null,
                OrderState = OrderState.Waiting,
                Deliverer = deliverer ?? null,
                DeliveryAddress = deliveryAddress,
                Date = DateTime.UtcNow
            };

            var success = _orderService.CreateOrder(orderEntity);

            foreach (DishDto dish in orderRecv.Dishes)
            {
                var dishEntity = new Dish
                {
                    Cost = dish.Cost,
                    Description = dish.Description,
                    RestaurantId = dish.RestaurantId,
                    Title = dish.Title,
                };

                dishEntity.Order = _orderService.GetOrderById(orderEntity.OrderId);
                if (dishEntity.Order == null)
                {
                    Logger.LogError("Failed get order: {0}", orderEntity.OrderId);
                }

                var dishSuccess = _dishService.CreateDish(dishEntity);
                if (!dishSuccess)
                {
                    Logger.LogError("Failed create dish: {0}", content);
                }
            }

            if (!success)
            {
                Logger.LogError("Failed create order: {0}", content);
            }

            Logger.LogInformation("Succes create order");
        }
    }
}
