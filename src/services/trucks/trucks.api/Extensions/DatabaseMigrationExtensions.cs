using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Polly;
using Trucks.Infrastructure.Sql;

namespace trucks.api.Extensions
{
    public static class DatabaseMigrationExtensions
    {
        public static void RunDatabaseMigrations(this IServiceProvider servicesProvider)
        {
            using var scope = servicesProvider.CreateScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<TrucksDbContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<TrucksDbContext>>();

            var retries = 10;
            var retry = Policy.Handle<SqlException>()
                .WaitAndRetry(
                    retryCount: retries,
                    sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    onRetry: (exception, timeSpan, r, ctx) =>
                    {
                        logger.LogWarning(exception, "[{prefix}] Exception {ExceptionType} with message {Message} detected on attempt {r} of {retries}, Connection string: {connectionString}", nameof(TrucksDbContext), exception.GetType().Name, exception.Message, r, retries, dbContext.Database.GetConnectionString());
                    });

            // if the sql server container is not created on run docker compose this
            // migration can't fail for network related exception. The retry options for DbContext only 
            // apply to transient exceptions
            // Note that this is NOT applied when running some orchestrators (let the orchestrator to recreate the failing service)
            retry.Execute(() => dbContext.Database.Migrate());
        }
    }
}
