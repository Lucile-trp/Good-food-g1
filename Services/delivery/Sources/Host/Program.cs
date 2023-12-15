using Host;
using RabbitMQ;
using RabbitMQ.Client;
using RabbitMQ.Connection;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton(pc =>
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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.UseRabbitListener();

app.Run();