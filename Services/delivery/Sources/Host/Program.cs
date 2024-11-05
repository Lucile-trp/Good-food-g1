using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using RabbitMQ;
using RabbitMQ.Client;
using RabbitMQ.Connection;
using RabbitMQ.EventBus;
using Host.Core;
using Host.Interfaces.Repository;
using Host.Interfaces.Services;
using Host.Services;
using Host.Helpers;
using Host.Repository;
using Host.Data;
using Host.Models;
using Host.Extensions;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<DeliveryDbContext>(options =>
{
    options.UseNpgsql(Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection"));
});


builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IDeliveryAddressService, DeliveryAddressService>();

builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IDeliveryAddressRepository, DeliveryAddressRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllersWithViews()
                .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


builder.Services.AddHttpContextAccessor();

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

builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
});

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