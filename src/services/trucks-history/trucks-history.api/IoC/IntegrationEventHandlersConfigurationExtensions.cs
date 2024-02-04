using TrucksHistory.Application.IntegrationEvents.EventHandlers;

namespace trucks_history.api.IoC;

public static class IntegrationEventHandlersConfigurationExtensions
{
    public static IServiceCollection AddIntegrationEventHandlers(this IServiceCollection services)
    {
        services.AddTransient<TruckCreatedIntegrationEventHandler>();
        services.AddTransient<TruckStatusChangedIntegrationEventHandler>();
        services.AddTransient<TruckUpdateIntegrationEventHandler>();

        return services;
    }
}
