using FluentResults;
using MediatR;
using Trucks.domain.Trucks;

namespace trucks.application.Commands;

internal class ReturnCommandHandler : IRequestHandler<ReturnCommand, Result>
{
    private readonly ITrucksRepository _trucksRepository;

    public ReturnCommandHandler(ITrucksRepository trucksRepository)
    {
        _trucksRepository = trucksRepository;
    }

    public async Task<Result> Handle(ReturnCommand request, CancellationToken cancellationToken)
    {
        var truckRes = await _trucksRepository.GetAsync(request.TruckId, cancellationToken);
        if (truckRes.IsFailed)
            return truckRes.ToResult();

        var statusChangeRes = truckRes.Value.Return();
        if (statusChangeRes.IsFailed)
            return statusChangeRes.ToResult();

        _trucksRepository.Update(truckRes.Value);

        return Result.Ok();
    }
}
