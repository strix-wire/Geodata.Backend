using Geodata.Persistence.EntityTypeConfigurations;
using Geodata.Persistence.IdentityEF;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Geodata.Application.Interfaces;
using Geodata.Domain;
using Microsoft.AspNetCore.Identity;

namespace Geodata.Persistence;

public class GeodataDbContext : IdentityDbContext<MyIdentityUser>, IGeodataDbContext
{
    public DbSet<GeodataDomain> GeodataEntities { get; set; }

    public GeodataDbContext(DbContextOptions<GeodataDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new GeoEventConfiguration());
        builder.ApplyConfiguration(new IdentityConfiguration());
        base.OnModelCreating(builder);

        //to do put in a separate class
        string idRoleAdmin = Guid.NewGuid().ToString();
        string idRoleUser = Guid.NewGuid().ToString();
        string idRoleModerator = Guid.NewGuid().ToString();
        
        //Create roles
        builder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Id = idRoleAdmin,
            NormalizedName = "ADMIN",
            Name = IdentityEF.UserRoles.Admin,
        });

        builder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Id = idRoleUser,
            NormalizedName = "USER",
            Name = IdentityEF.UserRoles.User,
        });

        builder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Id = idRoleModerator,
            NormalizedName = "MODERATOR",
            Name = IdentityEF.UserRoles.Moderator,
        });
    }
}
