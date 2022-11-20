using Geodata.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Geodata.Application.Geodata.Commands.UpdateGeodata;

internal class UpdateGeodataCommandHandler : IRequestHandler<UpdateGeodataCommand>
{
    private readonly IGeodataDbContext _dbContext;

	public UpdateGeodataCommandHandler(IGeodataDbContext dbContext) =>
		_dbContext = dbContext;

    public async Task<Unit> Handle(UpdateGeodataCommand request,
            CancellationToken cancellationToken)
    {
        var entity =
            await _dbContext.GeodataEntities.FirstOrDefaultAsync(geoEvent =>
            geoEvent.Id == request.Id, cancellationToken);

        if (entity == null)
            //to (never) do create class exception
            //throw new NotFoundException(nameof(GeoEventConsidered), request.Id);
            throw new ArgumentNullException();

        //If entity is exist then update her
        entity.Details = request.Details;
        entity.Title = request.Title;
        entity.EditDate = DateTime.Now;
        entity.Latitude = request.Latitude;
        entity.Longitude = request.Longitude;
        entity.IsChecked = request.IsChecked;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
