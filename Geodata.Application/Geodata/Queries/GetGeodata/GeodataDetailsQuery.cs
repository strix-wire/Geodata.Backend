using MediatR;

namespace Geodata.Application.Geodata.Queries.GetGeodata;

public class GeodataDetailsQuery : IRequest<GeodataDetailsVm>
{
    public Guid UserId { get; set; }
    public Guid Id { get; set; }
}
