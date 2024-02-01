using EventBus.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;
using TrucksHistory.Application.Commands;
using TrucksHistory.Application.IntegrationEvents.Events;

namespace TrucksHistory.Application.IntegrationEvents.EventHandlers;
public class TruckUpdateIntegrationEventHandler : IIntegrationEventHandler<TruckUpdateIntegrationEvent>
{
    private readonly ILogger<TruckUpdateIntegrationEventHandler> _logger;
    private readonly IMediator _mediator;

    public TruckUpdateIntegrationEventHandler(ILogger<TruckUpdateIntegrationEventHandler> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    public async Task Handle(TruckUpdateIntegrationEvent @event)
    {
        _logger.LogInformation("TRUCK-HISTORY: HANDLING INTEGRATION EVENT (EVENT_ID: {0}, TRUCK ID: {1}", @event.Id, @event.TruckId);

        var command = new UpdateTruckHistoryCommand(@event.TruckId,
            new UpdateTruckHistoryCommand.UpdateTruckhistoryRequest(@event.TruckCode, @event.TruckName));
        await _mediator.Send(command);
    }
}
