using FluentResults;
using MediatR;

namespace trucks.application.Commands;

public class ReturnCommand : IRequest<Result>
{
    public Guid TruckId { get; }

    public ReturnCommand(Guid truckId)
    {
        TruckId = truckId;
    }
}
