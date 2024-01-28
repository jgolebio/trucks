using EventBus.Abstractions;
using Microsoft.Extensions.Logging;
using TrucksHistory.Application.IntegrationEvents.Events;

namespace TrucksHistory.Application.IntegrationEvents.EventHandlers;
public class TruckCreatedIntegrationEventHandler : IIntegrationEventHandler<TruckCreatedIntegrationEvent>
{
    private readonly ILogger<TruckCreatedIntegrationEventHandler> _logger;

    public TruckCreatedIntegrationEventHandler(ILogger<TruckCreatedIntegrationEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(TruckCreatedIntegrationEvent @event)
    {
        _logger.LogInformation("TRUCK-HISTORY: HANDLING INTEGRATION EVENT (EVENT_ID: {0}, TRUCK ID: {1}", @event.Id, @event.TruckId);

        return Task.CompletedTask;
    }
}
