using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Geodata.Persistence;

public static class DependencyInjection
{
    //Extension method for adding a database context to a web application and registering it
    public static IServiceCollection AddPersistence(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = Path.GetDirectoryName
            (Directory.GetCurrentDirectory()) + "\\Db" + "\\GeoEventSqlite.db" };
        var connectionString = connectionStringBuilder.ToString();
        var connection = new SqliteConnection(connectionString);

        services.AddDbContext<GeoEventsDbContext>(options =>
        {
            options.UseSqlite(connection);
        });
        services.AddScoped<IGeoEventsDbContext>(provider => provider.GetService<GeoEventsDbContext>());
        return services;
    }
}