using FluentResults;
using MediatR;

namespace trucks.application.Commands;

public class NotifyAtJobCommand : IRequest<Result>
{
    public Guid TruckId { get; }

    public NotifyAtJobCommand(Guid truckId)
    {
        TruckId = truckId;
    }
}