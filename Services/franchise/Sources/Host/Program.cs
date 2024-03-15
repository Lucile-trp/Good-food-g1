using Host;
using Host.DataBase;
using Host.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;
using RabbitMQ;
using RabbitMQ.Client;
using RabbitMQ.Connection;
using RabbitMQ.EventBus;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure PostgreSQL DbContext
builder.Services.AddDbContext<FranchiseDbContext>(options =>
{
    var connectionString = builder.Configuration["ConnectionStrings__DefaultConnection"];
    options.UseNpgsql(connectionString);
});

// Configure RabbitMQ Persistent Connection
builder.Services.AddHostedService<IRabbitMQPersistentConnection>(pc =>
{
    var settings = new RabbitMQSettings()
    {
        Hostname = builder.Configuration["RabbitMQ__Hostname"],
        Username = builder.Configuration["RabbitMQ__Username"],
        Password = builder.Configuration["RabbitMQ__Password"]
    };

    return new RabbitMQPersistentConnection(settings);
});

var app = builder.Build();

// Apply migrations and ensure database is created
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<FranchiseDbContext>();
    if (context.Database.GetPendingMigrations().Any())
    {
        context.Database.Migrate();
    }
}

// Subscribe to RabbitMQ event bus
var persistentConnection = app.Services.GetServices<IHostedService>()
    .OfType<IRabbitMQPersistentConnection>().Single();
var eventBus = new RabbitMQEventBus(persistentConnection, app.Services.GetRequiredService<ILogger>(), Queues.Order);
eventBus.Subscribe(new OrderHandler(persistentConnection, app.Services.GetRequiredService<ILogger>()));

// Enable Swagger in Development environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Map controllers
app.MapControllers();

app.Run();
