using Host.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Connection;
using RabbitMQ.EventBus;

namespace Host.Order
{
    [ApiController]
    [Route("deliveries")]

    public class OrderController : Controller
    {
        private readonly IRabbitMQEventBus eventBus;

        public OrderController(IServiceProvider serviceProvider, ILoggerFactory loggerFactory)
        {
            var persistentConnection = serviceProvider.GetServices<IHostedService>().OfType<IRabbitMQPersistentConnection>().Single();
            eventBus = new RabbitMQEventBus(persistentConnection, loggerFactory, Queues.Order);
            eventBus.Subscribe(new OrderHandler(persistentConnection, loggerFactory));
        }

        [HttpGet("")]
        public ActionResult Get()
        {
            return Ok("Hello World !");
        }

        [HttpPost("")]
        public ActionResult Send(Models.Order command)
        {
            eventBus.Publish(command);
            return Ok();
        }
    }
}
