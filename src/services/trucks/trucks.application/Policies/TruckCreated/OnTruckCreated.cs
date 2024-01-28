using EventBus.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;
using trucks.application.IntergationEvents.Events;
using trucks.domain.Events;

namespace trucks.application.Policies.TruckCreated;
public class OnTruckCreated : INotificationHandler<TruckCreatedDomainEvent>
{
    private readonly ILogger<OnTruckCreated> _logger;
    private readonly IEventBus _eventBus;

    public OnTruckCreated(ILogger<OnTruckCreated> logger, IEventBus eventBus)
    {
        _logger = logger;
        _eventBus = eventBus;
    }

    public Task Handle(TruckCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("---Handling TruckCreated Domain Event(TruckId: {0})---", notification.Id);
        var intergationEventId = Guid.NewGuid();
        var integrationEvent = new TruckCreatedIntegrationEvent(intergationEventId, DateTime.UtcNow, notification.Id,
            notification.Code, notification.Name, notification.StatusId, notification.Status);


        try
        {
            _eventBus.Publish(integrationEvent);

            _logger.LogInformation("Integration Handled Successfully (EventId: {0})", intergationEventId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error handling integration event for truck created (TruckId: {0})", notification.Id);
        }

        return Task.CompletedTask;
    }
}
