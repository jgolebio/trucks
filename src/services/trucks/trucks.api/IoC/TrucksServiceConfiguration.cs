using Trucks.Application.Behaviors;
using Trucks.Application.Services;
using Trucks.Application.Commands;
using Azure.Messaging;

namespace Trucks.API.IoC
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
