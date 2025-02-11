using Carter;

namespace Ordering.Application;

public static class DependencyInjections
{
    public static IServiceCollection AddWebServices(this IServiceCollection services)
    {
        services.AddCarter();

        return services;
    }

    public static WebApplication UseWebServices(this WebApplication app)
    {
        app.MapCarter();

        return app;
    }
}