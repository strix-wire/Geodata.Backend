using Geodata.Domain;
using Microsoft.EntityFrameworkCore;

namespace Geodata.Application.Interfaces;

public interface IGeodataDbContext
{
    DbSet<GeodataDomain> GeodataEntities { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
