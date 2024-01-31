using FluentResults;
using MediatR;
using TrucksHistory.Infrastructure.Postgres.Repositories;

namespace TrucksHistory.Application.Queries;
public class GetTrucksQueryHandler : IRequestHandler<GetTrucksQuery, Result<GetTrucksQuery.TrucksViewModel>>
{
    private readonly ITrucksHistoryRepository _trucksHistoryRepository;

    public GetTrucksQueryHandler(ITrucksHistoryRepository trucksHistoryRepository)
    {
        _trucksHistoryRepository = trucksHistoryRepository;
    }

    public async Task<Result<GetTrucksQuery.TrucksViewModel>> Handle(GetTrucksQuery request, CancellationToken cancellationToken)
    {
        var trucks = await _trucksHistoryRepository.GetAllAsync();

        return Result.Ok(new GetTrucksQuery.TrucksViewModel(trucks.Select(x => new GetTrucksQuery.TruckViewModel(x.Id, x.Code, x.Name,x.Status)).ToArray(),
            trucks.Count()));
    }
}
