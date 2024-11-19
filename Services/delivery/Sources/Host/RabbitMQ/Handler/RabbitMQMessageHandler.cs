using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Connection;

namespace Host.RabbitMQ.Handler
{
#nullable enable
    public abstract class RabbitMQMessageHandler
    {
        private readonly IRabbitMQPersistentConnection PersistentConnection;
        public RabbitMQMessageHandler(IRabbitMQPersistentConnection persistentConnection)
        {
            PersistentConnection = persistentConnection;
        }

        public void OnConsumerReceived(object? sender, BasicDeliverEventArgs ea)
        {
            var content = System.Text.Encoding.UTF8.GetString(ea.Body.ToArray());
            HandleMessage(content);
        }

        public abstract void HandleMessage(string content);
    }
}
