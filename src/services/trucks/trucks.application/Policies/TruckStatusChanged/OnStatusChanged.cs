using EventBus.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;
using Trucks.Application.IntergationEvents.Events;
using Trucks.Domain.Events;

namespace Trucks.Application.Policies.TruckStatusChanged;

public class OnStatusChanged<T> : INotificationHandler<T> where T : StatusChangedDomainEvent
{
    private readonly ILogger<T> _logger;
    private readonly IEventBus _eventBus;

    public OnStatusChanged(ILogger<T> logger, IEventBus eventBus)
    {
        _logger = logger;
        _eventBus = eventBus;
    }

    public Task Handle(T notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("---Handling Truck Status Changed Domain Event(TruckId: {0})---", notification.Id);
        var intergationEventId = Guid.NewGuid();
        var integrationEvent = new TruckStatusChangedIntegrationEvent(intergationEventId, notification.Id, notification.Status,
            notification.StatusId, DateTime.UtcNow);

        try
        {
            _eventBus.Publish(integrationEvent);

            _logger.LogInformation("Integration Handled Successfully (EventId: {0})", intergationEventId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error handling integration event for truck status changed (TruckId: {0})", notification.Id);
        }

        return Task.CompletedTask;
    }
}
