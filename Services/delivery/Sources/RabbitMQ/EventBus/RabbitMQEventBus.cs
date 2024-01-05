using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Connection;
using System.Text;

namespace RabbitMQ.EventBus
{
    public class RabbitMQEventBus : IEventBus
    { 
        public readonly string QueueName;

        private IRabbitMQPersistentConnection PersistentConnection;

        public RabbitMQEventBus(IRabbitMQPersistentConnection persistentConnection, string queueName)
        {
            PersistentConnection = persistentConnection;
            QueueName = queueName;
        }
        public void Publish(object message)
        {
            if (!PersistentConnection.IsConnected)
            {
                PersistentConnection.TryConnect();
            }

            using var channel = PersistentConnection.CreateModel();

            channel.QueueDeclare(queue: QueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            string body = JsonConvert.SerializeObject(message);
            var bytes = Encoding.UTF8.GetBytes(body);

            channel.BasicPublish(exchange: string.Empty, routingKey: QueueName, basicProperties: null, body: bytes);
            channel.WaitForConfirmsOrDie();

            channel.BasicAcks += (sender, eventArgs) =>
            {
                Console.WriteLine("Sent RabbitMQ");
                //implement ack handle
            };
        }

        public void Subscribe<T>() where T : IIntegrationEventHandler
        {
            throw new NotImplementedException();
        }
    }
}
