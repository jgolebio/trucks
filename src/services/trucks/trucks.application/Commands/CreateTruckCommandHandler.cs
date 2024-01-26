using FluentResults;
using MediatR;
using Trucks.domain.Trucks;

namespace Trucks.application.Commands
{
    public class CreateTruckCommandHandler : IRequestHandler<CreateTruckCommand, Result<CreateTruckCommand.CreateTruckResult>>
    {
        private readonly ITrucksRepository _trucksRepository;

        public CreateTruckCommandHandler(ITrucksRepository trucksRepository)
        {
            _trucksRepository = trucksRepository;
        }

        public Task<Result<CreateTruckCommand.CreateTruckResult>> Handle(CreateTruckCommand request, CancellationToken cancellationToken)
        {
            var createTruckRes = Truck.Create(Guid.NewGuid(), request.Payload.Code, request.Payload.Name, request.Payload.Description);
            if (createTruckRes.IsFailed)
                return Task.FromResult<Result<CreateTruckCommand.CreateTruckResult>>(createTruckRes.ToResult());

            _trucksRepository.Add(createTruckRes.Value);

            return Task.FromResult<Result<CreateTruckCommand.CreateTruckResult>>(Result.Ok(
                new CreateTruckCommand.CreateTruckResult(createTruckRes.Value.Id.Value, "Truck created sucessfully")));
        }
    }
}
