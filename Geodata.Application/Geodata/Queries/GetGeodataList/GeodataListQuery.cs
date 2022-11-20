using MediatR;

namespace Geodata.Application.Geodata.Queries.GetGeodataList;

internal class GeodataListQuery : IRequest<GeodataListVm>
{
    public Guid UserId { get; set; }
}
