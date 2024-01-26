using FluentResults;
using MediatR;
using Trucks.domain.Trucks;
using Trucks.Infrastructure.Sql.Repositories;

namespace Trucks.application.Commands
{
    public class CreateTruckCommandHandler : IRequestHandler<CreateTruckCommand, Result<CreateTruckCommand.CreateTruckResult>>
    {
        private readonly ITrucksRepository _trucksRepository;

        public CreateTruckCommandHandler(ITrucksRepository trucksRepository)
        {
            _trucksRepository = trucksRepository;
        }

        public async Task<Result<CreateTruckCommand.CreateTruckResult>> Handle(CreateTruckCommand request, CancellationToken cancellationToken)
        {
            await Task.Delay(1);

            var createTruckRes = Truck.Create(request.Payload.Code, request.Payload.Name, request.Payload.Description);
            if (createTruckRes.IsFailed)
                return createTruckRes.ToResult();

            _trucksRepository.Add(createTruckRes.Value.ToDbSnapshot());

            return Result.Ok(new CreateTruckCommand.CreateTruckResult(createTruckRes.Value.Id, "Truck created sucessfully"));
        }
    }
}
