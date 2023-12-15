using Host.Core.Models;

namespace RabbitMQ.EventBus
{
    public interface IEventBus
    {
        void Publish(object message);

        void Subscribe<T>()
            where T : IIntegrationEventHandler<ModelBase>;
    }
}
