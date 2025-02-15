using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Application;

public static class DependencyInjections
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, 
        IConfiguration configuration)
    {

        //var connectionString = configuration.GetConnectionString("Database");

        //services.AddDbContext<,>(opt =>
        //{
        //    opt.UseSqlServer(connectionString);
        //});

        //services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

        return services;
    }
}