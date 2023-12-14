using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

Receive(Console.WriteLine);

void Receive(Action<string> received)
{
    var factory = new ConnectionFactory { HostName = "localhost", Port = 5672 };
    using var connection = factory.CreateConnection();
    using var channel = connection.CreateModel();

    channel.QueueDeclare(queue: "hello",
                         durable: false,
                         exclusive: false,
                         autoDelete: false,
                         arguments: null);

    Console.WriteLine(" [*] Waiting for messages.");

    var consumer = new EventingBasicConsumer(channel);
    consumer.Received += (model, ea) =>
    {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        received(message);
    };

    channel.BasicConsume(queue: "hello",
                         autoAck: true,
                         consumer: consumer);
}
