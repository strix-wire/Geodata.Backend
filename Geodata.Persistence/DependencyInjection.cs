using Geodata.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Geodata.Persistence;

public static class DependencyInjection
{
    //Extension method for adding a database context to a web application and registering it
    public static IServiceCollection AddPersistence(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connection = configuration.GetConnectionString("geoevent_db");

        services.AddDbContext<GeodataDbContext>(options =>
        {
            options.UseNpgsql(connection);
        });
        services.AddScoped<IGeodataDbContext>(provider => provider.GetService<GeodataDbContext>());
        return services;
    }
}