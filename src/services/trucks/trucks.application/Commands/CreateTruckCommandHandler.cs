using FluentResults;
using MediatR;
using trucks.domain.Trucks;

namespace trucks.application.Commands
{
    public class CreateTruckCommandHandler : IRequestHandler<CreateTruckCommand, Result<CreateTruckCommand.CreateTruckResult>>
    {
        public async Task<Result<CreateTruckCommand.CreateTruckResult>> Handle(CreateTruckCommand request, CancellationToken cancellationToken)
        {
            await Task.Delay(1);

            var createTruckRes = Truck.Create(request.Payload.Code, request.Payload.Name, request.Payload.Description);
            if (createTruckRes.IsFailed)
                return createTruckRes.ToResult();

            return Result.Ok(new CreateTruckCommand.CreateTruckResult(createTruckRes.Value.Id, "Truck created sucessfully"));
        }
    }
}
