using Host.Dto.Rpc;
using Host.RabbitMQ.Handler;
using Newtonsoft.Json;
using RabbitMQ.Connection;
namespace Host.Handlers
{
    public class OrderingHandler : RabbitMQMessageHandler
    {
        private readonly ILogger Logger;

        public OrderingHandler(IRabbitMQPersistentConnection persistentConnection, ILoggerFactory loggerFactory) : base(persistentConnection)
        {
            Logger = loggerFactory.CreateLogger<OrderingHandler>();
        }

        internal OrderingHandler(IRabbitMQPersistentConnection persistentConnection, ILogger logger) : base(persistentConnection)
        {
            Logger = logger;
        }

        public override void HandleMessage(string content)
        {
            Logger.LogInformation($"Order consuming Message");
            Logger.LogInformation(string.Concat("Message: ", content));

            var orderRecv = JsonConvert.DeserializeObject<OrderRecvDto>(content);

            Logger.LogInformation($"The dish is {orderRecv.Dish[0].Title}");
        }
    }
}
