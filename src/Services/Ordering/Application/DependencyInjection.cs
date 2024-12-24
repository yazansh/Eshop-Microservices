using BuildingBlocks.Behaviors;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Ordering.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(configs =>
        {
            configs.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            configs.AddOpenBehavior(typeof(LoggingBehavior<,>));
            configs.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        return services;
    }
}
