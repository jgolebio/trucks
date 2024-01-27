using FluentResults;
using MediatR;
using trucks.application.Behaviors;

namespace trucks.application.Commands;

public class UpdateTruckCommand : BaseCommand, IRequest<Result>
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
