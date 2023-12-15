using Host.Core.Models;

namespace RabbitMQ.EventBus
{
    public interface IIntegrationEventHandler<T> where T : ModelBase
    {

    }
}