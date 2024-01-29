using FluentResults;
using MediatR;
using Trucks.Application.Behaviors;

namespace Trucks.Application.Commands;

public class NotifyAtJobCommand : BaseCommand, IRequest<Result>
{
    public Guid TruckId { get; }

    public NotifyAtJobCommand(Guid truckId)
    {
        TruckId = truckId;
    }
}