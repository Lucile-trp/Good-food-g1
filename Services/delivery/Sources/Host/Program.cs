using Host;
using Host.Core;
using Host.Order;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ;
using RabbitMQ.Client;
using RabbitMQ.Connection;
using RabbitMQ.EventBus;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHostedService<IRabbitMQPersistentConnection>(pc =>
{
    var settings = builder.Configuration.GetSection("RabbitMQ").Get<RabbitMQSettings>();

    if (settings == null)
    {
        throw new ArgumentNullException($"{nameof(settings)} nout found in appsettings.json");
    }

    settings.Ip = Environment.GetEnvironmentVariable("RabbitMQ_IP") ?? settings.Ip;

    var factory = new ConnectionFactory() { HostName = settings.Ip, Port = settings.Port };

    return new RabbitMQPersistentConnection(factory);
});

var app = builder.Build();

// TODO: Delete
var persistentConnection = app.Services.GetServices<IHostedService>().OfType<IRabbitMQPersistentConnection>().Single();
var eventBus = new RabbitMQEventBus(persistentConnection, app.Logger, Queues.Order);
eventBus.Subscribe(new OrderHandler(persistentConnection, app.Logger));

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.UseRabbitListener();

app.Run();