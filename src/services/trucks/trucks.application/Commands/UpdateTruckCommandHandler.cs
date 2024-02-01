using FluentResults;
using MediatR;
using Trucks.Application.Services;
using Trucks.Domain.Trucks;

namespace Trucks.Application.Commands;

public class UpdateTruckCommandHandler : IRequestHandler<UpdateTruckCommand, Result>
{
    private readonly ITrucksRepository _trucksRepository;
    private readonly ITruckUniqueCodeService _truckUniqueCodeService;

    public UpdateTruckCommandHandler(ITrucksRepository trucksRepository, ITruckUniqueCodeService truckUniqueCodeService)
    {
        _trucksRepository = trucksRepository;
        _truckUniqueCodeService = truckUniqueCodeService;
    }

    public async Task<Result> Handle(UpdateTruckCommand request, CancellationToken cancellationToken)
    {


        var truckRes = await _trucksRepository.GetAsync(request.TruckId, cancellationToken);
        if (truckRes.IsFailed)
            return truckRes.ToResult();

        if (truckRes.Value.Code.Value != request.Payload.Code)
        {
            var checkRes = await IsNewCodeUnique(request.Payload.Code);
            if (checkRes.IsFailed)
                return checkRes.ToResult();

            if (!checkRes.Value)
                return Result.Fail("Truck code should be unique");
        }

        var updateRes = truckRes.Value.Update(request.Payload.Code, request.Payload.Name, request.Payload.Description);
        if (updateRes.IsFailed)
            return updateRes.ToResult();

        _trucksRepository.Update(truckRes.Value);

        return Result.Ok();
    }

    private async Task<Result<bool>> IsNewCodeUnique(string code)
    {
        var isUniqueCodeRes = await _truckUniqueCodeService.IsUniqueAsync(code);
        if (isUniqueCodeRes.IsFailed)
            return isUniqueCodeRes.ToResult();

        return Result.Ok(!isUniqueCodeRes.Value);
    }
}
