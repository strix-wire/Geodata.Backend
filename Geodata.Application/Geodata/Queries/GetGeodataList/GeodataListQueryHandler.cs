using AutoMapper;
using AutoMapper.QueryableExtensions;
using Geodata.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Geodata.Application.Geodata.Queries.GetGeodataList;

internal class GeodataListQueryHandler
    : IRequestHandler<GeodataListQuery, GeodataListVm>
{
    private readonly IGeodataDbContext _dbContext;
    private readonly IMapper _mapper;

    public GeodataListQueryHandler(IGeodataDbContext dbContext, IMapper mapper) =>
        (_dbContext, _mapper) = (dbContext, mapper);

    public async Task<GeodataListVm> Handle(GeodataListQuery request,
        CancellationToken cancellationToken)
    {
        var GeoEventsQuery = await _dbContext.GeodataEntities
            //to do role = moderator or admin - see all
            //else only IsChecked == false

            //Метод расширения из automapper, который проецирует коллекцию в соответствии с заданной
            //конфигурацией
            .ProjectTo<GeodataLookupDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new GeodataListVm { GeoEvents = GeoEventsQuery };
    }
}
