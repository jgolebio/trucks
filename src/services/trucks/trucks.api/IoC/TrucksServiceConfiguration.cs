using trucks.application.Commands;

namespace trucks.api.IoC
{
    public static class TrucksServiceConfiguration
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(CreateTruckCommand)));
        }
    }
}
