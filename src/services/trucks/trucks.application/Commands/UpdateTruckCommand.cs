using FluentResults;
using MediatR;
using Trucks.Application.Behaviors;

namespace Trucks.Application.Commands;

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
