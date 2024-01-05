using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;

namespace RabbitMQ.Connection
{
    public class RabbitMQPersistentConnection : IRabbitMQPersistentConnection
    {
        private readonly IConnectionFactory ConnectionFactory;
        IConnection Connection;
        bool Disposed;

        public bool IsConnected => Connection != null && Connection.IsOpen && !Disposed;

        public RabbitMQPersistentConnection(IConnectionFactory connectionFactory)
        {
            ConnectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
            TryConnect();
        }

        public void CreateConsumersChannels()
        {
            if (!IsConnected)
            {
                TryConnect();
            }

            //Channel 01
            _eventBusRabbitMQ = new EventBusRabbitMQ(this, "create");
            _eventBusRabbitMQ.CreateConsumerChannel();

        }

        public IModel CreateModel()
        {
            if (!IsConnected)
            {
                throw new InvalidOperationException("No RabbitMQ connections are available to perform this action");
            }
            return Connection.CreateModel();
        }

        public void Disconnect()
        {
            if (Disposed)
            {
                return;
            }
            Dispose();
        }

        public void Dispose()
        {
            if (Disposed) return;

            Disposed = true;

            try
            {
                Connection.Dispose();
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public bool TryConnect()
        {
            try
            {
                Console.WriteLine("RabbitMQ Client is trying to connect");
                Connection = ConnectionFactory.CreateConnection();
            }
            catch (BrokerUnreachableException e)
            {
                Thread.Sleep(5000);
                Console.WriteLine("RabbitMQ Client is trying to reconnect");
                Connection = ConnectionFactory.CreateConnection();
            }

            if (IsConnected)
            {
                Console.WriteLine($"RabbitMQ persistent connection acquired a connection {Connection.Endpoint.HostName} and is subscribed to failure events");
                return true;
            }
            else
            {
                Console.WriteLine("RabbitMQ connections could not be created and opened");
                return false;
            }
        }
    }
}
