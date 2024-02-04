using FluentResults;
using MediatR;
using TrucksHistory.Infrastructure.Postgres.DbModels;
using TrucksHistory.Infrastructure.Postgres.Repositories;

namespace TrucksHistory.Application.Commands;

public class AddTruckHistoryEntryCommandHandler : IRequestHandler<AddTruckHistoryEntryCommand, Result>
{
    private readonly ITrucksHistoryRepository _trucksHistoryRepository;

    public AddTruckHistoryEntryCommandHandler(ITrucksHistoryRepository trucksHistoryRepository)
    {
        _trucksHistoryRepository = trucksHistoryRepository;
    }

    public async Task<Result> Handle(AddTruckHistoryEntryCommand request, CancellationToken cancellationToken)
    {
        var dbModel = await _trucksHistoryRepository.FindByIdAsync(request.TruckId);
        if (dbModel is null)
            return Result.Fail("Object not found for given id");

        var entry = new TruckHistoryEntryDbModel
        {
            TruckId = dbModel.Id,
            EntryDate = request.Payload.EntryDate,
            Status = request.Payload.Status,
            StatusCode = request.Payload.StatusCode
        };

        dbModel.Status = request.Payload.Status;
        dbModel.StatusCode = request.Payload.StatusCode;
        dbModel.Entries.Add(entry);

        _trucksHistoryRepository.UpdateDbModel(dbModel);

        return Result.Ok();
    }
}
