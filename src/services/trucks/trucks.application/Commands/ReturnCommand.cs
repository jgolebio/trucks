using FluentResults;
using MediatR;
using Trucks.Application.Behaviors;

namespace Trucks.Application.Commands;

public class ReturnCommand : BaseCommand, IRequest<Result>
{
    public Guid TruckId { get; }

    public ReturnCommand(Guid truckId)
    {
        TruckId = truckId;
    }
}
