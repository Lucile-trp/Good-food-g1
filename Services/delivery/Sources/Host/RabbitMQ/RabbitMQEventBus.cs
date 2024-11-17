using Host.RabbitMQ.Handler;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Connection;
using System.Text;

namespace RabbitMQ.EventBus
{
    public class RabbitMQEventBus : IRabbitMQEventBus
    { 
        public readonly string QueueName;

        private IRabbitMQPersistentConnection PersistentConnection;
        private IModel Channel;
        private ILogger Logger;

        public RabbitMQEventBus(IRabbitMQPersistentConnection persistentConnection, ILoggerFactory loggerFactory, string queueName)
        {
            PersistentConnection = persistentConnection;
            QueueName = queueName;
            Channel = PersistentConnection.Channel;
            Logger = loggerFactory.CreateLogger<RabbitMQEventBus>();
        }

        public void Publish(object message)
        {
            if (!PersistentConnection.IsConnected)
            {
                PersistentConnection.TryConnect();
            }

            string body = JsonConvert.SerializeObject(message);
            Logger.LogInformation(body);
            var bytes = Encoding.UTF8.GetBytes(body);

            var props = Channel.CreateBasicProperties();
            props.ContentType = "application/json";

            Channel.BasicPublish(exchange: "", routingKey: QueueName, props, body: bytes);
        }

        public void Subscribe(RabbitMQMessageHandler eventHandler)
        {
            ArgumentNullException.ThrowIfNull(eventHandler);
            ArgumentNullException.ThrowIfNull(QueueName);

            if (!PersistentConnection.Subscribers.ContainsKey(QueueName))
            {
                PersistentConnection.Subscribers.Add(QueueName, eventHandler);
            }
        }

        public void Unsubscribe(RabbitMQMessageHandler eventHandler)
        {
            ArgumentNullException.ThrowIfNull(eventHandler);
            ArgumentNullException.ThrowIfNull(QueueName);

            if (PersistentConnection.Subscribers.ContainsKey(QueueName))
            {
                PersistentConnection.Subscribers.Remove(QueueName);
            }
        }

        public void Dispose()
        {
            Channel.Dispose();
        }
    }
}
