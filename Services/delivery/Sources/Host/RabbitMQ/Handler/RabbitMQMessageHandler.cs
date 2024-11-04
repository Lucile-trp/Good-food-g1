using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Connection;

namespace Host.RabbitMQ.Handler
{
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
            PersistentConnection.Channel.BasicAck(ea.DeliveryTag, false);
        }
        public void OnConsumerConsumerCancelled(object? sender, ConsumerEventArgs e) 
        {

        }
        public void OnConsumerUnregistered(object? sender, ConsumerEventArgs e) 
        { 
            
        }
        public void OnConsumerRegistered(object? sender, ConsumerEventArgs e) 
        { 
            
        }
        public void OnConsumerShutdown(object? sender, ShutdownEventArgs e) 
        { 
            
        }

        public abstract void HandleMessage(string content);
    }
}
