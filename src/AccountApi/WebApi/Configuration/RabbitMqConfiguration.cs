using AccountApi.Infrastructure.Helpers.Consumer;
using MassTransit;
using Newtonsoft.Json;
using Shared.Events;

namespace AccountApi.WebApi.Configuration
{
    public static class RabbitMqConfiguration
    {
        public static void AddRabbitMqWithDLX(this IServiceCollection services, IConfiguration configuration)
        {
            using ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
            ILogger logger = factory.CreateLogger("DLQ");


            services.AddMassTransit(x =>
                {
                    //x.SetKebabCaseEndpointNameFormatter();

                    x.AddConsumer<UserRegisteredConsumer>();
                    x.AddConsumer<UserRegistrationFailedConsumer>();

                    x.UsingRabbitMq((context, cfg) =>
                    {

                        cfg.Host(configuration.GetSection("RabbitOptions:Url").Value, h =>
                        {
                            h.Username(configuration.GetSection("RabbitOptions:User").Value);
                            h.Password(configuration.GetSection("RabbitOptions:Password").Value);
                        });

                        cfg.ReceiveEndpoint("user-registered-queue", e =>
                        {
                            e.ConfigureConsumer<UserRegisteredConsumer>(context);

                            // Retry ayarı
                            e.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(2)));


                        });

                        
                        cfg.ReceiveEndpoint("user-registered-failed-queue", e =>
                        {
                            e.ConfigureConsumer<UserRegistrationFailedConsumer>(context);

                        });

                        //cfg.ConfigureEndpoints(context, new KebabCaseEndpointNameFormatter("accountapi", false));
                        

                    });
                });
            services.AddHealthChecks();
        }
    }

}
