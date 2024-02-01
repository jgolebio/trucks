using FluentResults;
using MediatR;
using TrucksHistory.Infrastructure.Postgres.DbModels;
using TrucksHistory.Infrastructure.Postgres.Repositories;

namespace TrucksHistory.Application.Commands;
public class CreateTruckHistoryCommandHandler : IRequestHandler<CreateTruckHistoryCommand, Result<CreateTruckHistoryCommand.CreateTruckHistoryResult>>
{
    private readonly ITrucksHistoryRepository _trucksHistoryRepository;

    public CreateTruckHistoryCommandHandler(ITrucksHistoryRepository trucksHistoryRepository)
    {
        _trucksHistoryRepository = trucksHistoryRepository;
    }

    public Task<Result<CreateTruckHistoryCommand.CreateTruckHistoryResult>> Handle(CreateTruckHistoryCommand request, CancellationToken cancellationToken)
    {
        var truckHistoryDbModel = new TruckHistoryDbModel
        {
            Id = request.Payload.TruckId,
            Code = request.Payload.Code,
            Name = request.Payload.Name,
            Status = request.Payload.Status,
            StatusCode = request.Payload.StatusCode,
            CreateDate = request.Payload.CreateDate
        };

        _trucksHistoryRepository.Add(truckHistoryDbModel);

        return Task.FromResult<Result<CreateTruckHistoryCommand.CreateTruckHistoryResult>>(Result.Ok(
            new CreateTruckHistoryCommand.CreateTruckHistoryResult(truckHistoryDbModel.Id, "Truck history created successfully")));
    }
}
