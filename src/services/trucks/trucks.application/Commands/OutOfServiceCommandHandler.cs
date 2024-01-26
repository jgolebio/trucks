using FluentResults;
using MediatR;
using Trucks.domain.Trucks;

namespace trucks.application.Commands;

public class OutOfServiceCommandHandler : IRequestHandler<OutOfServiceCommand, Result>
{
    private readonly ITrucksRepository _trucksRepository;

    public OutOfServiceCommandHandler(ITrucksRepository trucksRepository)
    {
        _trucksRepository = trucksRepository;
    }

    public async Task<Result> Handle(OutOfServiceCommand request, CancellationToken cancellationToken)
    {
        var truckRes = await _trucksRepository.GetAsync(request.TruckId, cancellationToken);
        if (truckRes.IsFailed)
            return truckRes.ToResult();

        var statusChangeRes = truckRes.Value.ChangeToOutOfService();
        if (statusChangeRes.IsFailed)
            return statusChangeRes.ToResult();

        _trucksRepository.Update(truckRes.Value);

        return Result.Ok();
    }
}
