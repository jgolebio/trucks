using FluentResults;
using MediatR;

namespace trucks.application.Commands;

public class DeleteTruckCommand : IRequest<Result>
{
    public Guid TruckId { get; }

    public DeleteTruckCommand(Guid truckId)
    {
        TruckId = truckId;
    }
}
