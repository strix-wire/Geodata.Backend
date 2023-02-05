using Geodata.Application.Geodata.Commands.CreateGeodata;
using Geodata.Application.Interfaces;
using Geodata.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Geodata.Application.Geodata.Commands.AcceptCheckedGeoData;

internal class AcceptCheckedGeoDataHandler : IRequestHandler<AcceptCheckedGeoData>
{
    private readonly IGeodataDbContext _dbContext;

    public AcceptCheckedGeoDataHandler(IGeodataDbContext dbContext) =>
        _dbContext = dbContext;

    public async Task<Guid> Handle(AcceptCheckedGeoData request,
            CancellationToken cancellationToken)
    {
        // НАДО ЗАПРОСИТЬ ИЗ БАЗЫ ТУТ СДЕЛАЙ

    //    var entity =
    //await _dbContext.GeodataEntities.FirstOrDefaultAsync(geoEvent =>
    //geoEvent.Id == request.Id, cancellationToken);

    //    if (entity == null)
    //        //to (never) do create class exception
    //        //throw new NotFoundException(nameof(GeoEventConsidered), request.Id);
    //        throw new ArgumentNullException();

    //    //If entity is exist then update her
    //    entity.Details = request.Details;
    //    entity.Title = request.Title;
    //    entity.EditDate = DateTime.Now;
    //    entity.Latitude = request.Latitude;
    //    entity.Longitude = request.Longitude;
    //    entity.IsChecked = request.IsChecked;

    //    await _dbContext.SaveChangesAsync(cancellationToken);
        var geoEvent = new GeodataDomain
        {
            Title = request.Title,
            Details = request.Details,
            Latitude = request.Latitude,
            Longitude = request.Longitude,
            IsChecked = true,
            EditDate = DateTime.Now
        };

        await _dbContext.GeodataEntities.Update
    }
}
