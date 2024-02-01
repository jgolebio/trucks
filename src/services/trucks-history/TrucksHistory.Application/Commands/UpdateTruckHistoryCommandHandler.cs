using FluentResults;
using MediatR;
using TrucksHistory.Infrastructure.Postgres.Repositories;

namespace TrucksHistory.Application.Commands;

public class UpdateTruckHistoryCommandHandler : IRequestHandler<UpdateTruckHistoryCommand, Result>
{
    private readonly ITrucksHistoryRepository _trucksHistoryRepository;

    public UpdateTruckHistoryCommandHandler(ITrucksHistoryRepository trucksHistoryRepository)
    {
        _trucksHistoryRepository = trucksHistoryRepository;
    }

    public async Task<Result> Handle(UpdateTruckHistoryCommand request, CancellationToken cancellationToken)
    {
        var dbModel = await _trucksHistoryRepository.FindByIdAsync(request.TruckId);
        if (dbModel is null)
            return Result.Fail("Object not found for given id");

        dbModel.Code = request.Payload.Code;
        dbModel.Name = request.Payload.Name;

        _trucksHistoryRepository.UpdateDbModel(dbModel);

        return Result.Ok();
    }
}
