using FluentResults;
using MediatR;
using trucks.application.Behaviors;

namespace trucks.application.Commands;

public class SendToJobCommand : BaseCommand, IRequest<Result>
{
    public Guid TruckId { get; }

    public SendToJobCommand(Guid truckId)
    {
        TruckId = truckId;
    }
}
