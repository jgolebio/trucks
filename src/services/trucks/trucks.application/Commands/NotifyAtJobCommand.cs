using FluentResults;
using MediatR;
using trucks.application.Behaviors;

namespace trucks.application.Commands;

public class NotifyAtJobCommand : BaseCommand, IRequest<Result>
{
    public Guid TruckId { get; }

    public NotifyAtJobCommand(Guid truckId)
    {
        TruckId = truckId;
    }
}