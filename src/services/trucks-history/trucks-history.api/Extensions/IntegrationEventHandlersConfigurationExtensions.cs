using TrucksHistory.Application.IntegrationEvents.EventHandlers;

namespace trucks_history.api.Extensions;

public static class IntegrationEventHandlersConfigurationExtensions
{
    public static IServiceCollection AddIntegrationEventHandlers(this IServiceCollection services)
    {
        services.AddTransient<TruckCreatedIntegrationEventHandler>();

        return services;
    }
}
