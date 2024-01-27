using FluentResults;
using MediatR;
using trucks.application.Behaviors;

namespace Trucks.application.Commands;

public class CreateTruckCommand : BaseCommand, IRequest<Result<CreateTruckCommand.CreateTruckResult>>
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
