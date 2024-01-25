using MediatR;

namespace trucks.api.Apis
{
    public class TrucksService(IMediator mediator)
    {
        public IMediator Mediator { get; } = mediator;
    }
}
