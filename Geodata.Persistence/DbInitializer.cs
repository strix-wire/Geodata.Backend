namespace Geodata.Persistence;

public class DbInitializer
{
    public static void Initialize(GeoEventsDbContext context)
    {
        context.Database.EnsureCreated();
    }
}