using Dapper;
using FluentResults;
using System.Text;
using Trucks.Infrastructure.Sql.Extensions;
using static Trucks.Infrastructure.Sql.Queries.ITrucksQueries;

namespace Trucks.Infrastructure.Sql.Queries;

public class TrucksQueries : ITrucksQueries
{
    private readonly TrucksDbContext _dbContext;

    public TrucksQueries(TrucksDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<IEnumerable<ITrucksQueries.TruckReadModel>>> FindAsync(int page, int pageSize,
        TrucksFilter filter, string? orderBy, bool? isAscending)
    {
        await using var sqlServerConnection = _dbContext.GetSqlConnection();

        StringBuilder sqlQuery = new StringBuilder("select Id,Code,Name,Description,Status from trc.Trucks");

        AddFilters(sqlQuery, filter);

        if (!string.IsNullOrEmpty(orderBy))
        {
            var sortOrder = isAscending is null || isAscending.Value ? "asc" : "desc";
            sqlQuery.Append($" order by {orderBy} {sortOrder}");
        }

        var offset = (page - 1) * pageSize;
        sqlQuery.Append($" OFFSET {offset} ROWS FETCH NEXT {pageSize} ROWS ONLY");

        try
        {
            var queryRes = await sqlServerConnection.QueryAsync<ITrucksQueries.TruckReadModel>(sqlQuery.ToString());

            return Result.Ok(queryRes);
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    private void AddFilters(StringBuilder sqlQuery, TrucksFilter filter)
    {
        var whereClause = new StringBuilder();
        if (!string.IsNullOrEmpty(filter.Code))
            whereClause.Append($" Code like '%{filter.Code}%' and");

        if (!string.IsNullOrEmpty(filter.Name))
            whereClause.Append($" Name like '%{filter.Name}%' and");

        if (filter.Status is not null)
            whereClause.Append($" Status={filter.Status} and");

        if (whereClause.Length == 0)
            return;

        sqlQuery.Append($" where {whereClause.ToString().Substring(0, whereClause.Length - 3)}");
    }
}
