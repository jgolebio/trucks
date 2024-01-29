using FluentResults;
using MediatR;
using Trucks.Domain.Trucks;

namespace Trucks.Application.Commands;

public class StartLoadingCommandHandler : IRequestHandler<StartLoadingCommand, Result>
{
    private readonly ITrucksRepository _trucksRepository;

    public StartLoadingCommandHandler(ITrucksRepository trucksRepository)
    {
        _trucksRepository = trucksRepository;
    }

    public async Task<Result> Handle(StartLoadingCommand request, CancellationToken cancellationToken)
    {
        var truckRes = await _trucksRepository.GetAsync(request.TruckId, cancellationToken);
        if (truckRes.IsFailed)
            return truckRes.ToResult();

        var statusChangeRes = truckRes.Value.StartLoadingTruck();
        if (statusChangeRes.IsFailed)
            return statusChangeRes.ToResult();

        _trucksRepository.Update(truckRes.Value);

        return Result.Ok();
    }
}
