using Geodata.Persistence.EntityTypeConfigurations;
using Geodata.Persistence.IdentityEF;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Geodata.Application.Interfaces;
using Geodata.Domain;

namespace Geodata.Persistence;

public class GeodataDbContext : IdentityDbContext<MyIdentityUser>, IGeodataDbContext
{
    public DbSet<GeodataDomain> GeodataEntities { get; set; }

    public GeodataDbContext(DbContextOptions<GeodataDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new GeoEventConfiguration());
        base.OnModelCreating(builder);
    }
}
