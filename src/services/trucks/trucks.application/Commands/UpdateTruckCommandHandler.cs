using FluentResults;
using MediatR;
using Trucks.domain.Trucks;

namespace trucks.application.Commands;

public class UpdateTruckCommandHandler : IRequestHandler<UpdateTruckCommand, Result>
{
    private readonly ITrucksRepository _trucksRepository;

    public UpdateTruckCommandHandler(ITrucksRepository trucksRepository)
    {
        _trucksRepository = trucksRepository;
    }

    public async Task<Result> Handle(UpdateTruckCommand request, CancellationToken cancellationToken)
    {
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
