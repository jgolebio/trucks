using EventBus.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;
using TrucksHistory.Application.Commands;
using TrucksHistory.Application.IntegrationEvents.Events;

namespace TrucksHistory.Application.IntegrationEvents.EventHandlers;
public class TruckCreatedIntegrationEventHandler : IIntegrationEventHandler<TruckCreatedIntegrationEvent>
{
    private readonly ILogger<TruckCreatedIntegrationEventHandler> _logger;
    private readonly IMediator _mediator;

    public TruckCreatedIntegrationEventHandler(ILogger<TruckCreatedIntegrationEventHandler> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    public async Task Handle(TruckCreatedIntegrationEvent @event)
    {
        _logger.LogInformation("TRUCK-HISTORY: HANDLING INTEGRATION EVENT (EVENT_ID: {0}, TRUCK ID: {1}", @event.Id, @event.TruckId);

        var command = new CreateTruckHistoryCommand(new CreateTruckHistoryCommand.CreateTruckHistoryRequest(@event.TruckId, @event.TruckCode,
            @event.TruckName, @event.CreationDate, @event.Status, @event.StatusId));
        await _mediator.Send(command);
    }
}
