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
        var isUniqueCodeRes = await _truckUniqueCodeService.IsUniqueAsync(request.Payload.Code);
        if (isUniqueCodeRes.IsFailed)
            return isUniqueCodeRes.ToResult();

        if (!isUniqueCodeRes.Value)
            return Result.Fail("Truck code must be unique");

        var truckRes = await _trucksRepository.GetAsync(request.TruckId, cancellationToken);
        if (truckRes.IsFailed)
            return truckRes.ToResult();

        var updateRes = truckRes.Value.Update(request.Payload.Code, request.Payload.Name, request.Payload.Description);
        if (updateRes.IsFailed)
            return updateRes.ToResult();

        _trucksRepository.Update(truckRes.Value);

        return Result.Ok();
    }
}
