using FluentResults;
using MediatR;

namespace trucks.application.Commands;

public class CreateTruckCommand : IRequest<Result<CreateTruckCommand.CreateTruckResult>>
{
    public record CreateTruckResult(Guid Id, string Message);
    public record CreateTruckRequest(string Code, string Name, string Description);

    public CreateTruckRequest Payload { get; }

    public CreateTruckCommand(CreateTruckRequest payload)
    {
        Payload = payload;
    }
}
