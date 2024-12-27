using BuildingBlocks.Behaviors;
using BuildingBlocks.Messaging.MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Ordering.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(configs =>
        {
            configs.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            configs.AddOpenBehavior(typeof(LoggingBehavior<,>));
            configs.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        services.AddMessageBroker(configuration, Assembly.GetExecutingAssembly());

        return services;
    }
}
