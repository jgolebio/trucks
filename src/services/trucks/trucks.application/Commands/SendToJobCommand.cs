using FluentResults;
using MediatR;

namespace trucks.application.Commands;

public class SendToJobCommand : IRequest<Result>
{
    public Guid TruckId { get; }

    public SendToJobCommand(Guid truckId)
    {
        TruckId = truckId;
    }
}
