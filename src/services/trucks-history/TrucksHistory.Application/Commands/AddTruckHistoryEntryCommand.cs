using FluentResults;
using MediatR;
using TrucksHistory.Application.Behaviours;

namespace TrucksHistory.Application.Commands;

public class AddTruckHistoryEntryCommand : BaseCommand, IRequest<Result>
{
    public record class AddTruckHistoryEntryRequest(DateTime EntryDate, string Status, int StatusCode);

    public Guid TruckId { get; set; }
    public AddTruckHistoryEntryRequest Payload { get; set; }

    public AddTruckHistoryEntryCommand(Guid truckId, AddTruckHistoryEntryRequest payload)
    {
        TruckId = truckId;
        Payload = payload;
    }
}
