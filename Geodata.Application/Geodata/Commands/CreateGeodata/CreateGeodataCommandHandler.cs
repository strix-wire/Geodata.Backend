using Geodata.Application.Interfaces;
using Geodata.Domain;
using MediatR;

namespace Geodata.Application.Geodata.Commands.CreateGeodata;

public class CreateGeodataCommandHandler : IRequestHandler<CreateGeodataCommand, Guid>
{
    private readonly IGeodataDbContext _dbContext;

    public CreateGeodataCommandHandler(IGeodataDbContext dbContext) =>
        _dbContext = dbContext;

    public async Task<Guid> Handle(CreateGeodataCommand request,
            CancellationToken cancellationToken)
    {
        var geoEvent = new GeodataDomain
        {
            UserId = request.UserId,
            Id = Guid.NewGuid(),
            Title = request.Title,
            Details = request.Details,
            Latitude = request.Latitude,
            Longitude = request.Longitude,
            IsChecked = false,
            CreationDate = DateTime.Now,
            EditDate = null
        };

        await _dbContext.GeodataEntities.AddAsync(geoEvent, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return geoEvent.Id;
    }
}
