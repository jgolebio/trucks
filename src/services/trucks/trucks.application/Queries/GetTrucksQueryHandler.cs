using FluentResults;
using MediatR;
using Trucks.Application.Queries.Extensions;
using Trucks.Infrastructure.Sql.Queries;

namespace Trucks.Application.Queries;

public class GetTrucksQueryHandler : IRequestHandler<GetTrucksQuery, Result<GetTrucksQuery.TrucksViewModel>>
{
    private readonly ITrucksQueries _trucksQueries;

    public GetTrucksQueryHandler(ITrucksQueries trucksQueries)
    {
        _trucksQueries = trucksQueries;
    }

    public async Task<Result<GetTrucksQuery.TrucksViewModel>> Handle(GetTrucksQuery request, CancellationToken cancellationToken)
    {
        var filter = new ITrucksQueries.TrucksFilter(request.Payload.Code, request.Payload.Name, request.Payload.Status);
        var page = request.Payload.Page ?? 1;
        var pageSize = request.Payload.PageSize ?? 50;
        var orderBy = request.Payload.OrderBy ?? "Code";
        var trucksReadModelRes = await _trucksQueries.FindAsync(page, pageSize, filter, orderBy, request.Payload.IsAcending);
        
        if (trucksReadModelRes.IsFailed)
            return trucksReadModelRes.ToResult();

        return Result.Ok(trucksReadModelRes.Value.ToViewModel());
    }
}
