using FluentResults;
using MediatR;
using trucks.application.Behaviors;

namespace trucks.application.Commands;

public class OutOfServiceCommand : BaseCommand, IRequest<Result>
{
    public Guid TruckId { get; }

    public OutOfServiceCommand(Guid truckId)
    {
        TruckId = truckId;
    }
}
