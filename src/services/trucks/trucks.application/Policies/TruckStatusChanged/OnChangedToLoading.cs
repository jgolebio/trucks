using EventBus.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;
using Trucks.Application.IntergationEvents.Events;
using Trucks.Domain.Events;

namespace Trucks.Application.Policies.TruckStatusChanged;
public class OnChangedToLoading// : INotificationHandler<LoadingStartedDomainEvent>
{
    private readonly ILogger<OnChangedToLoading> _logger;
    private readonly IEventBus _eventBus;

    public OnChangedToLoading(ILogger<OnChangedToLoading> logger, IEventBus eventBus)
    {
        _logger = logger;
        _eventBus = eventBus;
    }

    public Task Handle(LoadingStartedDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("---Handling Truck Status Changed To Loading Event(TruckId: {0})---", notification.Id);
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
            _logger.LogError(ex, "Error handling integration event for truck status changed to laoding (TruckId: {0})", notification.Id);
        }

        return Task.CompletedTask;
    }
}
