using EventBus.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;
using Trucks.Application.IntergationEvents.Events;
using Trucks.Domain.Events;

namespace Trucks.Application.Policies.TruckUpdated;
public class OnTruckUpdated : INotificationHandler<TruckUpdatedDomainEvent>
{
    private readonly ILogger<OnTruckUpdated> _logger;
    private readonly IEventBus _eventBus;

    public OnTruckUpdated(ILogger<OnTruckUpdated> logger, IEventBus eventBus)
    {
        _logger = logger;
        _eventBus = eventBus;
    }

    public Task Handle(TruckUpdatedDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("---Handling TruckUpdated Domain Event(TruckId: {0})---", notification.Id);
        var intergationEventId = Guid.NewGuid();
        var integrationEvent = new TruckUpdateIntegrationEvent(intergationEventId, DateTime.UtcNow, notification.Id,
            notification.Code, notification.Name);

        try
        {
            _eventBus.Publish(integrationEvent);

            _logger.LogInformation("Integration Handled Successfully (EventId: {0})", intergationEventId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error handling integration event for truck update (TruckId: {0})", notification.Id);
        }

        return Task.CompletedTask;
    }
}
