using Host.Core;
using Host.RabbitMQ.Handler;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

namespace RabbitMQ.Connection
{
    public class RabbitMQPersistentConnection : BackgroundService, IRabbitMQPersistentConnection
    {

        public Dictionary<string, RabbitMQMessageHandler> Subscribers { get; }

        public IModel Channel { get; private set; }

        private readonly IConnectionFactory ConnectionFactory;
        private IConnection Connection;
        private bool Disposed;

        RabbitMQSettings Settings;

        public bool IsConnected => Connection != null && Connection.IsOpen && !Disposed;

        public RabbitMQPersistentConnection(RabbitMQSettings settings)
        {
            Settings = settings;
            Subscribers = new Dictionary<string, RabbitMQMessageHandler>();
            ConnectionFactory = new ConnectionFactory() 
            { 
                HostName = settings.Hostname, 
                Port = settings.Port, 
                UserName = settings.Username, 
                Password = settings.Password 
            };

            TryConnect();
        }

        public void CreateConsumersChannels()
        {
            if (!IsConnected)
            {
                TryConnect();
            }


            Channel.ExchangeDeclare("goodfood.exchange", ExchangeType.Topic);

            foreach (var subscriber in Subscribers)
            {
                Channel.QueueDeclare(subscriber.Key, false, false, false, null);
                Channel.QueueBind(subscriber.Key, "goodfood.exchange", string.Concat(Queues.QueueBase, "*"), null);
                Channel.BasicQos(0, 1, false);

                var consumer = new EventingBasicConsumer(Channel);

                consumer.Received += subscriber.Value.OnConsumerReceived;
                consumer.Shutdown += subscriber.Value.OnConsumerShutdown;
                consumer.Registered += subscriber.Value.OnConsumerRegistered;
                consumer.Unregistered += subscriber.Value.OnConsumerUnregistered;
                consumer.ConsumerCancelled += subscriber.Value.OnConsumerConsumerCancelled;

                Channel.BasicConsume(subscriber.Key, false, consumer);
            }
        }

        public void CreateModel()
        {
            if (!IsConnected)
            {
                throw new InvalidOperationException("No RabbitMQ connections are available to perform this action");
            }
            
            Channel = Connection.CreateModel();
        }

        public void Disconnect()
        {
            if (Disposed)
            {
                return;
            }

            Dispose();
        }

        public override void Dispose()
        {
            if (Disposed) return;

            Disposed = true;

            try
            {
                Connection.Dispose();
                Channel.Close();
                Channel.Dispose();
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
            catch (BrokerUnreachableException)
            {
                try
                {
                    Console.WriteLine("RabbitMQ Client is trying to reconnect");
                    Thread.Sleep(5000);
                    Connection = ConnectionFactory.CreateConnection();
                }
                catch (Exception)
                {
                    Console.WriteLine("RabbitMQ connections could not be created and opened");
                    Console.WriteLine($"Connection failed on: {Settings.Hostname}:{Settings.Port} with {ConnectionFactory.UserName}");
                    throw;
                }
            }

            if (IsConnected)
            {
                Console.WriteLine($"RabbitMQ persistent connection acquired a connection {Connection.Endpoint.HostName}");
                return true;
            }

            return false;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            CreateModel();
            CreateConsumersChannels();

            return Task.CompletedTask;
        }
    }
}
