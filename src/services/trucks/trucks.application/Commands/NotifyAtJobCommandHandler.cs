using FluentResults;
using MediatR;
using Trucks.domain.Trucks;

namespace trucks.application.Commands;

internal class NotifyAtJobCommandHandler : IRequestHandler<NotifyAtJobCommand, Result>
{
    private readonly ITrucksRepository _trucksRepository;

    public NotifyAtJobCommandHandler(ITrucksRepository trucksRepository)
    {
        _trucksRepository = trucksRepository;
    }
    
    public async Task<Result> Handle(NotifyAtJobCommand request, CancellationToken cancellationToken)
    {
        var truckRes = await _trucksRepository.GetAsync(request.TruckId, cancellationToken);
        if (truckRes.IsFailed)
            return truckRes.ToResult();

        var statusChangeRes = truckRes.Value.NotifyAtJob();
        if (statusChangeRes.IsFailed)
            return statusChangeRes.ToResult();

        _trucksRepository.Update(truckRes.Value);

        return Result.Ok();
    }
}
