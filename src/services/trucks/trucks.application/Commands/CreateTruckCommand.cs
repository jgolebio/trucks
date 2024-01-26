using FluentResults;
using MediatR;

namespace Trucks.application.Commands;

public class CreateTruckCommand : IRequest<Result<CreateTruckCommand.CreateTruckResult>>
{
    public record CreateTruckResult(Guid Id, string Message);
    public record CreateTruckRequest(string Code, string Name, string Description)
    {
        public CreateTruckRequest() : this(string.Empty, string.Empty, string.Empty) { }
    }

    public CreateTruckRequest Payload { get; }

    public CreateTruckCommand(CreateTruckRequest payload)
    {
        Payload = payload;
    }
}
