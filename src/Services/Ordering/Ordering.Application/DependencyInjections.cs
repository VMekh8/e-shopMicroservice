using System.Reflection;
using BuildingBlock.BehaviorPipeline;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Application;

public static class DependencyInjections
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(opt =>
        {
            opt.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            opt.AddOpenBehavior(typeof(ValidationBehavior<,>));
            opt.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });

        return services;
    }
}