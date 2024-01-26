using MediatR;

namespace Trucks.api.Apis
{
    public class TrucksService(IMediator mediator)
    {
        public IMediator Mediator { get; } = mediator;
    }
}
