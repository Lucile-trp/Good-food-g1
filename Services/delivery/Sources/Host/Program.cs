using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using RabbitMQ;
using RabbitMQ.Client;
using RabbitMQ.Connection;
using RabbitMQ.EventBus;
using Host.Core;
using Host.Order;
using Host.DataBase;
using Host.Models;
using Host.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<DeliveryDbContext>(options =>
{
    options.UseNpgsql(Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection"));
});


/*builder.Services.AddHostedService<IRabbitMQPersistentConnection>(pc =>
{
    var settings = new RabbitMQSettings()
    {
        Hostname = builder.Configuration["RabbitMQ:Hostname"],
        Username = builder.Configuration["RabbitMQ:Username"],
        Password = builder.Configuration["RabbitMQ:Password"],
        Port = int.Parse(builder.Configuration["RabbitMQ:Port"])
    };

    return new RabbitMQPersistentConnection(settings);
});*/

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

/*
// TODO: Delete
var persistentConnection = app.Services.GetServices<IHostedService>().OfType<IRabbitMQPersistentConnection>().Single();
var eventBus = new RabbitMQEventBus(persistentConnection, app.Logger, Queues.Order);
eventBus.Subscribe(new OrderHandler(persistentConnection, app.Logger));
*/

app.ApplyMigrations();

app.MapControllers();

app.Run();