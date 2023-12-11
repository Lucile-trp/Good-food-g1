var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/send", () =>
{
    RabbitMQ.RabbitBQ.Send("Hello world");
    return "Hello world";
});

app.Run();
