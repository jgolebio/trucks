using FluentResults;
using MediatR;
using trucks.application.Behaviors;

namespace trucks.application.Commands;

public class ReturnCommand : BaseCommand, IRequest<Result>
{
    public Guid TruckId { get; }

    public ReturnCommand(Guid truckId)
    {
        TruckId = truckId;
    }
}
