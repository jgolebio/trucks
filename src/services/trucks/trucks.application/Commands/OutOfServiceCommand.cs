using FluentResults;
using MediatR;
using Trucks.Application.Behaviors;

namespace Trucks.Application.Commands;

public class OutOfServiceCommand : BaseCommand, IRequest<Result>
{
    public Guid TruckId { get; }

    public OutOfServiceCommand(Guid truckId)
    {
        TruckId = truckId;
    }
}
