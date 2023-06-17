using MediatR;

namespace Geodata.Application.Geodata.Queries.GetGeodataList;

public class GeodataListQuery : IRequest<GeodataListVm>
{
    public Guid UserId { get; set; }
}
