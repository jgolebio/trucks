using EventBus.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;
using TrucksHistory.Application.Commands;
using TrucksHistory.Application.IntegrationEvents.Events;

namespace TrucksHistory.Application.IntegrationEvents.EventHandlers;
public class TruckStatusChangedIntegrationEventHandler : IIntegrationEventHandler<TruckStatusChangedIntegrationEvent>
{
    private readonly ILogger<TruckStatusChangedIntegrationEventHandler> _logger;
    private readonly IMediator _mediator;

    public TruckStatusChangedIntegrationEventHandler(ILogger<TruckStatusChangedIntegrationEventHandler> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    public async Task Handle(TruckStatusChangedIntegrationEvent @event)
    {
        _logger.LogInformation("TRUCK-HISTORY: HANDLING INTEGRATION EVENT (EVENT_ID: {0}, TRUCK ID: {1}", @event.Id, @event.TruckId);

        var command = new AddTruckHistoryEntryCommand(@event.TruckId,
            new AddTruckHistoryEntryCommand.AddTruckHistoryEntryRequest(@event.EventDate, @event.Status, @event.StatusId));

        await _mediator.Send(command);
    }
}
