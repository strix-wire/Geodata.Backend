using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Geodata.Persistence.IdentityEF;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Geodata.Persistence.EntityTypeConfigurations;

public class IdentityConfiguration : IEntityTypeConfiguration<MyIdentityUser>
{
    public void Configure(EntityTypeBuilder<MyIdentityUser> builder)
    {
        
    }
}
