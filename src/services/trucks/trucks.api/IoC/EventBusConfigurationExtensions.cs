using EventBus;
using EventBus.Abstractions;
using EventBusRabbitMQ;
using RabbitMQ.Client;

namespace Trucks.API.IoC;

public static class EventBusConfigurationExtensions
{
    public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
        {
            var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();


            var factory = new ConnectionFactory()
            {
                HostName = configuration["EventBusConnection"],
                DispatchConsumersAsync = true
            };

            if (!string.IsNullOrEmpty(configuration["EventBusUserName"]))
            {
                factory.UserName = configuration["EventBusUserName"];
            }

            if (!string.IsNullOrEmpty(configuration["EventBusPassword"]))
            {
                factory.Password = configuration["EventBusPassword"];
            }

            var retryCount = 5;
            if (!string.IsNullOrEmpty(configuration["EventBusRetryCount"]))
            {
                retryCount = int.Parse(configuration["EventBusRetryCount"]);
            }

            return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
        });

        services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

        var subscriptionClientName = configuration["SubscriptionClientName"];
        services.AddSingleton<IEventBus, EventBusRabbitMQ.EventBusRabbitMQ>(sp =>
        {
            var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
            var serviceScopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
            var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ.EventBusRabbitMQ>>();
            var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

            var retryCount = 5;
            if (!string.IsNullOrEmpty(configuration["EventBusRetryCount"]))
            {
                retryCount = int.Parse(configuration["EventBusRetryCount"]);
            }

            return new EventBusRabbitMQ.EventBusRabbitMQ(rabbitMQPersistentConnection, logger, serviceScopeFactory, eventBusSubcriptionsManager, subscriptionClientName, retryCount);
        });

        return services;
    }
}
