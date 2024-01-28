using FluentResults;
using MediatR;
using trucks.application.Services;
using Trucks.domain.Trucks;

namespace Trucks.application.Commands
{
    public class CreateTruckCommandHandler : IRequestHandler<CreateTruckCommand, Result<CreateTruckCommand.CreateTruckResult>>
    {
        private readonly ITrucksRepository _trucksRepository;
        private readonly ITruckUniqueCodeService _truckUniqueCodeService;

        public CreateTruckCommandHandler(ITrucksRepository trucksRepository, ITruckUniqueCodeService truckUniqueCodeService)
        {
            _trucksRepository = trucksRepository;
            _truckUniqueCodeService = truckUniqueCodeService;
        }

        public async Task<Result<CreateTruckCommand.CreateTruckResult>> Handle(CreateTruckCommand request, CancellationToken cancellationToken)
        {
            var isUniqueCodeRes = await _truckUniqueCodeService.IsUniqueAsync(request.Payload.Code);
            if (isUniqueCodeRes.IsFailed)
                return isUniqueCodeRes.ToResult();

            if (!isUniqueCodeRes.Value)
                return Result.Fail("Truck code must be unique");

            var createTruckRes = Truck.Create(Guid.NewGuid(), request.Payload.Code, request.Payload.Name, request.Payload.Description);
            if (createTruckRes.IsFailed)
                return createTruckRes.ToResult();

            _trucksRepository.Add(createTruckRes.Value);

            return Result.Ok(new CreateTruckCommand.CreateTruckResult(createTruckRes.Value.Id.Value, "Truck created sucessfully"));
        }
    }
}
