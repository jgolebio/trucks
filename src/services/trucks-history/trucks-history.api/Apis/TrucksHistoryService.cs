using MediatR;

namespace TrucksHistory.API.Apis;

public class TrucksHistoryService
{
    private readonly IMediator _mediator;
    public IMediator Mediator => _mediator;
    public TrucksHistoryService(IMediator mediator)
    {
        _mediator = mediator;
    }
}
