using Geodata.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Geodata.Persistence.EntityTypeConfigurations;

//This class need to decrease size method "OnModelCreating"
//If in project will appear new entities, then will be convenient
//to expand configuration
public class GeoEventConfiguration : IEntityTypeConfiguration<GeodataDomain>
{
    public void Configure(EntityTypeBuilder<GeodataDomain> builder)
    {
        //Id - primary key
        builder.HasKey(geoEvent => geoEvent.Id);
        //Id - unique
        builder.HasIndex(geoEvent => geoEvent.Id).IsUnique();
    }
}
