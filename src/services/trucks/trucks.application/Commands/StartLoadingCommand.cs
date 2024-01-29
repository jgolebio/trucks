using FluentResults;
using MediatR;
using Trucks.Application.Behaviors;

namespace Trucks.Application.Commands;

public class StartLoadingCommand : BaseCommand, IRequest<Result>
{
    public Guid TruckId { get; }

    public StartLoadingCommand(Guid truckId)
    {
        TruckId = truckId;
    }
}
