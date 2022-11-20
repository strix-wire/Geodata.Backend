using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Geodata.Persistence.IdentityEF;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Geodata.Persistence.EntityTypeConfigurations;

public class IdentityConfiguration : IdentityDbContext<MyIdentityUser>
{
    public void Configure(ModelBuilder builder)
    {
        string idRoleAdmin = Guid.NewGuid().ToString();
        string idRoleUser = Guid.NewGuid().ToString();
        string idRoleModerator = Guid.NewGuid().ToString();
        string idAccoundAdmin = Guid.NewGuid().ToString();

        //Create roles
        builder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Id = idRoleAdmin,
            NormalizedName = "ADMIN",
            Name = "Admin",
        });

        builder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Id = idRoleUser,
            NormalizedName = "USER",
            Name = "User",
        });

        builder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Id = idRoleModerator,
            NormalizedName = "MODERATOR",
            Name = "Moderator",
        });

        //Create admin
        var hasher = new PasswordHasher<MyIdentityUser>();
        builder.Entity<MyIdentityUser>().HasData(new MyIdentityUser
        {
            Id = idAccoundAdmin,
            UserName = "admin@gmail.com",
            NormalizedUserName = "ADMIN@GMAIL.COM",
            NormalizedEmail = "ADMIN@GMAIL.COM",
            Name = "Admin",
            Email = "admin@gmail.com",
            Surname = "AdminSurname",
            MiddleName = "AdminMiddleName",
            //DateOfBirth = model.DateOfBirth,
            City = "Tomsk",
            PasswordHash = hasher.HashPassword(null, "5tgmL1.2Ls"),
            //Sex = model.Sex
        });

        builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
        {
            RoleId = idRoleAdmin,
            UserId = idAccoundAdmin
        });

        builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
        {
            RoleId = idRoleUser,
            UserId = idAccoundAdmin
        });
    }
}
