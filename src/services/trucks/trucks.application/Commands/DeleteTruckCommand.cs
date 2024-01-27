using FluentResults;
using MediatR;
using trucks.application.Behaviors;

namespace trucks.application.Commands;

public class DeleteTruckCommand : BaseCommand, IRequest<Result>
{
    public Guid TruckId { get; }

    public DeleteTruckCommand(Guid truckId)
    {
        TruckId = truckId;
    }
}
