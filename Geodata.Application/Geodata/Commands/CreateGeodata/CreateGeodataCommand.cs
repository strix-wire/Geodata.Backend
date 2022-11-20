using MediatR;

namespace Geodata.Application.Geodata.Commands.CreateGeodata;

public class CreateGeodataCommand : IRequest<Guid>
{
    public Guid UserId { get; set; }
    public string Title { get; set; }
    public string Details { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }
}
