using AutoMapper;
using Geodata.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Geodata.Application.Geodata.Queries.GetGeodata;

internal class GeodataDetailsQueryHandler
    : IRequestHandler<GeodataDetailsQuery, GeodataDetailsVm>
{
    private readonly IGeodataDbContext _dbContext;
    private readonly IMapper _mapper;

    public GeodataDetailsQueryHandler(IGeodataDbContext dbContext, IMapper mapper)
    => (_dbContext, _mapper) = (dbContext, mapper);

    public async Task<GeodataDetailsVm> Handle(GeodataDetailsQuery request,
            CancellationToken cancellationToken)
    {
        var entity = await _dbContext.GeodataEntities
            .FirstOrDefaultAsync(geoEvent =>
            geoEvent.Id == request.Id, cancellationToken);

        if (entity == null)
            //to (never) do create class exception
            //throw new NotFoundException(nameof(GeoEventConsidered), request.Id);
            throw new ArgumentNullException();

        return _mapper.Map<GeodataDetailsVm>(entity);
    }
}
