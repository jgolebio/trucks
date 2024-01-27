using Trucks.domain.SeedWork;
using Trucks.domain.Trucks;
using Trucks.Infrastructure.Sql.Queries;

namespace trucks.application.Queries.Extensions
{
    internal static class TruckQueriesExtensions
    {
        public static GetTrucksQuery.TrucksViewModel ToViewModel(this IEnumerable<ITrucksQueries.TruckReadModel> trucksReadModel) =>
            new GetTrucksQuery.TrucksViewModel(trucksReadModel.Count(),
                trucksReadModel.Select(x => x.ToViewModel()).ToArray());

        public static GetTrucksQuery.TruckViewModel ToViewModel(this ITrucksQueries.TruckReadModel readModel) =>
            new GetTrucksQuery.TruckViewModel(readModel.Id, readModel.Code, readModel.Name, readModel.Description, readModel.Status,
                GetStatus(readModel.Status));

        private static string GetStatus(int status) =>
            Enumeration.FromValue<TruckStatus>(status).Name;
    }
}
