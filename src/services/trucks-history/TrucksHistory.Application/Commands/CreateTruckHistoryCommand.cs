using FluentResults;
using MediatR;
using TrucksHistory.Application.Behaviours;

namespace TrucksHistory.Application.Commands;
public class CreateTruckHistoryCommand : BaseCommand,IRequest<Result<CreateTruckHistoryCommand.CreateTruckHistoryResult>>
{
    public record class CreateTruckHistoryResult(Guid Id, string Message);
    public record class CreateTruckHistoryRequest(Guid TruckId, string Code, string Name, DateTime CreateDate, string Status, int StatusCode);

    public CreateTruckHistoryRequest Payload { get; }

    public CreateTruckHistoryCommand(CreateTruckHistoryRequest payload)
    {
        Payload = payload;
    }
}
