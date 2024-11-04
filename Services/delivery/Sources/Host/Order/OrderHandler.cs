using Host.RabbitMQ.Handler;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Connection;
using System.Text;

namespace Host.Order
{
    public class OrderHandler : RabbitMQMessageHandler
    {
        private readonly ILogger Logger;

        public OrderHandler(IRabbitMQPersistentConnection persistentConnection, ILoggerFactory loggerFactory) : base(persistentConnection)
        {
            Logger = loggerFactory.CreateLogger<OrderHandler>();
        }

        internal OrderHandler(IRabbitMQPersistentConnection persistentConnection, ILogger logger) : base(persistentConnection)
        {
            Logger = logger;
        }

        public override void HandleMessage(string content)
        {
            Logger.LogInformation($"Order consuming Message");
            Logger.LogInformation(string.Concat("Message: ", content));
        }
    }
}
