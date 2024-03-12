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
    var settings = new RabbitMQSettings()
    {
        Hostname = builder.Configuration["RabbitMQ:Hostname"],
        Username = builder.Configuration["RabbitMQ:Username"],
        Password = builder.Configuration["RabbitMQ:Password"],
        Port = int.Parse(builder.Configuration["RabbitMQ:Port"])
    };

    return new RabbitMQPersistentConnection(settings);
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