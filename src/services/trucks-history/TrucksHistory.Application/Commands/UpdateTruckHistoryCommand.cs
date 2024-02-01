using FluentResults;
using MediatR;
using TrucksHistory.Application.Behaviours;

namespace TrucksHistory.Application.Commands;
public class UpdateTruckHistoryCommand : BaseCommand, IRequest<Result>
{
    public record class UpdateTruckhistoryRequest(string Code, string Name);

    public UpdateTruckhistoryRequest Payload { get; }
    public Guid TruckId { get; }

    public UpdateTruckHistoryCommand(Guid truckId, UpdateTruckhistoryRequest payload)
    {
        TruckId = truckId;
        Payload = payload;
    }
}
