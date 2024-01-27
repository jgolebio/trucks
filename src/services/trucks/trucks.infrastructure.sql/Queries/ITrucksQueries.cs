using FluentResults;

namespace Trucks.Infrastructure.Sql.Queries;

public interface ITrucksQueries
{
    public record TruckReadModel(Guid Id, string Code, string Name, string Description, int Status);
    public record TrucksFilter(string? Code, string? Name, int? Status);

    Task<Result<IEnumerable<TruckReadModel>>> FindAsync(int page, int pageSize, TrucksFilter filter, string? orderBy, bool? isAscending);
}
