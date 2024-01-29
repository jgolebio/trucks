using FluentResults;
using MediatR;
using Trucks.Application.Behaviors;

namespace Trucks.Application.Commands;

public class SendToJobCommand : BaseCommand, IRequest<Result>
{
    public Guid TruckId { get; }

    public SendToJobCommand(Guid truckId)
    {
        TruckId = truckId;
    }
}
