using trucks.application.Behaviors;
using trucks.application.Services;
using Trucks.application.Commands;

namespace Trucks.api.IoC
{
    public static class TrucksServiceConfiguration
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining(typeof(CreateTruckCommand));

                cfg.AddOpenBehavior(typeof(TransactionBehavior<,>));
            });

            services.AddScoped<ITruckUniqueCodeService, TruckUniqueCodeService>();
        }
    }
}
