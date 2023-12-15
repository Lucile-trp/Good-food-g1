using RabbitMQ.Connection;

namespace Host
{
    public static class ApplicationBuilderExtentions
    {
        public static RabbitMQPersistentConnection Listener { get; set; }

        public static IApplicationBuilder UseRabbitListener(this IApplicationBuilder app)
        {
            Listener = app.ApplicationServices.GetService<RabbitMQPersistentConnection>() ?? throw new ArgumentNullException(nameof(RabbitMQPersistentConnection));
            var life = app.ApplicationServices.GetService<IHostApplicationLifetime>() ?? throw new ArgumentNullException(nameof(IHostApplicationLifetime));
            life.ApplicationStarted.Register(OnStarted);

            //press Ctrl+C to reproduce if your app runs in Kestrel as a console app
            life.ApplicationStopping.Register(OnStopping);
            return app;
        }

        private static void OnStarted()
        {
            Listener.CreateConsumersChannels();
        }

        private static void OnStopping()
        {
            Listener.Disconnect();
        }
    }
}
