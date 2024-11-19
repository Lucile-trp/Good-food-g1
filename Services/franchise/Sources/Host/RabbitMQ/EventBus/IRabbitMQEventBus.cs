using Host.Core.Models;
using Host.RabbitMQ.Handler;

namespace RabbitMQ.EventBus
{
    public interface IRabbitMQEventBus : IDisposable
    {
        void Publish(object message);

        void Subscribe(RabbitMQMessageHandler eventHandler);

        void Unsubscribe(RabbitMQMessageHandler eventHandler);
    }
}
