using Asp.Versioning;
using AutoMapper;
using Host.Dto;
using Host.Dto.Rpc;
using Host.Handlers;
using Host.Interfaces.Services;
using Host.Models;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ;
using RabbitMQ.Connection;
using RabbitMQ.EventBus;

namespace Host.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class OrderingController : Controller
    {
        private readonly IRabbitMQEventBus eventBusGetDish;
        private readonly IRabbitMQEventBus eventBusSendDish;
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderingController(IServiceProvider serviceProvider, ILoggerFactory loggerFactory, IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;

            var persistentConnection = serviceProvider.GetServices<IHostedService>().OfType<IRabbitMQPersistentConnection>().Single();
            eventBusGetDish = new RabbitMQEventBus(persistentConnection, loggerFactory, Queues.GetDish);
            eventBusGetDish.Subscribe(new OrderingHandler(persistentConnection, loggerFactory));

            eventBusSendDish = new RabbitMQEventBus(persistentConnection, loggerFactory, Queues.SendDish);
        }
        
        [HttpPost]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult CreateOrder(IEnumerable<int> dishesIds)
        {
            if (dishesIds == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var orderEntity = new Order() {
                Date = DateTime.UtcNow
            };

            _orderService.CreateOrder(orderEntity);

            foreach (var dishId in dishesIds)
            {
                eventBusSendDish.Publish(new OrderingSenderDto() { DishId = dishId, OrderId = orderEntity.OrderId});
            }

            return Ok();
        }
    }
}