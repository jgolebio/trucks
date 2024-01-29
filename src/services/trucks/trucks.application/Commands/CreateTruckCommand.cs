using FluentResults;
using MediatR;
using Trucks.Application.Behaviors;

namespace Trucks.Application.Commands;

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
