using Host.RabbitMQ.Handler;
using RabbitMQ.Client;

namespace RabbitMQ.Connection
{
    public interface IRabbitMQPersistentConnection : IHostedService, IDisposable
    {
        Dictionary<string, RabbitMQMessageHandler> Subscribers { get; }

        bool IsConnected { get; }
        IModel Channel { get; }

        bool TryConnect();

        void CreateModel();

        void CreateConsumersChannels();

        void Disconnect();
    }
}
