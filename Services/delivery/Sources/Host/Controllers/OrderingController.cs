using Asp.Versioning;
using AutoMapper;
using Host.Dto;
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
        private readonly IOrderingService _orderingService;
        private readonly IMapper _mapper;

        public OrderingController(IServiceProvider serviceProvider, ILoggerFactory loggerFactory, IOrderingService orderingService, IMapper mapper)
        {
            _orderingService = orderingService;
            _mapper = mapper;

            var persistentConnection = serviceProvider.GetServices<IHostedService>().OfType<IRabbitMQPersistentConnection>().Single();
            eventBusGetDish = new RabbitMQEventBus(persistentConnection, loggerFactory, Queues.GetDish);
            eventBusGetDish.Subscribe(new OrderingHandler(persistentConnection, loggerFactory));
        }

        //RABBITMQ
        [HttpPost("send")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<OrderingDto>))]
        public ActionResult Send(OrderingDto order)
        {
            eventBusGetDish.Publish(order);
            return Ok();
        }
    }
}