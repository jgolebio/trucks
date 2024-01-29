using FluentResults;
using MediatR;
using Trucks.Application.Behaviors;

namespace Trucks.Application.Commands;

public class DeleteTruckCommand : BaseCommand, IRequest<Result>
{
    public Guid TruckId { get; }

    public DeleteTruckCommand(Guid truckId)
    {
        TruckId = truckId;
    }
}
