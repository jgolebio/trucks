using FluentResults;
using MediatR;

namespace trucks.application.Commands;

public class OutOfServiceCommand : IRequest<Result>
{
    public Guid TruckId { get; }

    public OutOfServiceCommand(Guid truckId)
    {
        TruckId = truckId;
    }
}
