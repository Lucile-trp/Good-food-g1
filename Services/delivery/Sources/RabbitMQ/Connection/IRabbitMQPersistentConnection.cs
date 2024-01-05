using RabbitMQ.Client;

namespace RabbitMQ.Connection
{
    public interface IRabbitMQPersistentConnection : IDisposable
    {
        bool IsConnected { get; }

        bool TryConnect();

        IModel CreateModel();

        void CreateConsumersChannels();

        void Disconnect();
    }
}
