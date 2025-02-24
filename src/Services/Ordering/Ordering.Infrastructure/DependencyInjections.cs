using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Infrastructure.Data;
using Ordering.Infrastructure.Data.Interceptors;

namespace Ordering.Application;

public static class DependencyInjections
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, 
        IConfiguration configuration)
    {

        var connectionString = configuration.GetConnectionString("Database");

        services.AddDbContext<ApplicationDbContext>(opt =>
        {
            opt.AddInterceptors(new AuditableEntityInterceptor());
            opt.UseSqlServer(connectionString);
        });

        //services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

        return services;
    }
}