namespace Geodata.Persistence;

public class DbInitializer
{
    public static void Initialize(GeodataDbContext context)
    {
        context.Database.EnsureCreated();
    }
}