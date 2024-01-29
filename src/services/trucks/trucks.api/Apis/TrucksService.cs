using MediatR;

namespace Trucks.API.Apis
{
    public class TrucksService(IMediator mediator)
    {
        public IMediator Mediator { get; } = mediator;
    }
}
