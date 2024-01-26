using FluentResults;
using MediatR;
using Trucks.domain.Trucks;

namespace trucks.application.Commands;

public class DeleteTruckCommandHandler : IRequestHandler<DeleteTruckCommand, Result>
{
    private readonly ITrucksRepository _trucksRepository;

    public DeleteTruckCommandHandler(ITrucksRepository trucksRepository)
    {
        _trucksRepository = trucksRepository;
    }

    public async Task<Result> Handle(DeleteTruckCommand request, CancellationToken cancellationToken)
    {
        var truckRes = await _trucksRepository.GetAsync(request.TruckId, cancellationToken);
        if (truckRes.IsFailed)
            return truckRes.ToResult();

        _trucksRepository.Delete(truckRes.Value);

        return Result.Ok();
    }
}
