using Trucks.Domain.Trucks;
using Trucks.Infrastructure.Sql.Queries;
using Trucks.Infrastructure.Sql.Repositories;

namespace Trucks.API.IoC
{
    public static class DatabaseConfigurationExtensions
    {
        public static void ConfigureDatabaseServices(this IServiceCollection services)
        {
            services.AddScoped<ITrucksRepository, TrucksRepository>();
            services.AddScoped<ITrucksQueries, TrucksQueries>();
        }
    }
}
