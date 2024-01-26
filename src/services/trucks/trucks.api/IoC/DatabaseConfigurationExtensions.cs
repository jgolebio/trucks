﻿using Trucks.domain.Trucks;
using Trucks.Infrastructure.Sql.Repositories;

namespace trucks.api.IoC
{
    public static class DatabaseConfigurationExtensions
    {
        public static void ConfigureDatabaseServices(this IServiceCollection services)
        {
            services.AddScoped<ITrucksRepository, TrucksRepository>();
        }
    }
}
