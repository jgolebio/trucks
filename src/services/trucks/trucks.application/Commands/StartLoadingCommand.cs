using FluentResults;
using MediatR;

namespace trucks.application.Commands;

public class StartLoadingCommand : IRequest<Result>
{
    public Guid TruckId { get; }

    public StartLoadingCommand(Guid truckId)
    {
        TruckId = truckId;
    }
}
