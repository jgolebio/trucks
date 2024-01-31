using Microsoft.EntityFrameworkCore;
using TrucksHistory.Infrastructure.Postgres;
using TrucksHistory.Infrastructure.Postgres.Repositories;

namespace TrucksHistory.API.IoC;

public static class ConfigureTruckHistoryDatabase
{
    public static IServiceCollection AddTrucksHistoryDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TrucksHistoryDbContext>(
                options => options.UseNpgsql(configuration.GetConnectionString("Postgres"),
                postresAction => postresAction.MigrationsAssembly("TrucksHistory.API")));
        //options => options.UseNpgsql(configuration.GetConnectionString("Postgres")));
        services.AddTransient<ITrucksHistoryRepository, TrucksHistoryRepository>();

        return services;
    }
}
