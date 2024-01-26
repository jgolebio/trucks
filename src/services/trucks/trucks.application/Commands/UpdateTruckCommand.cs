using FluentResults;
using MediatR;

namespace trucks.application.Commands;

public class UpdateTruckCommand : IRequest<Result>
{
    public record UpdtaeTruckRequest(string Code, string Name, string Description);

    public Guid TruckId { get; }
    public UpdtaeTruckRequest Payload { get; }

    public UpdateTruckCommand(Guid truckId, UpdtaeTruckRequest payload)
    {
        TruckId = truckId;
        Payload = payload;
    }
}
