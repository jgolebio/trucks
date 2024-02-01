using TrucksHistory.Application.Behaviours;
using TrucksHistory.Application.Queries;

namespace TrucksHistory.API.IoC;

public static class MediatRConfigurationExtensions
{
    public static IServiceCollection AddMediatr(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining(typeof(GetTrucksQuery));

            cfg.AddOpenBehavior(typeof(TransactionBehavior<,>));
        });

        return services;
    }
} 
