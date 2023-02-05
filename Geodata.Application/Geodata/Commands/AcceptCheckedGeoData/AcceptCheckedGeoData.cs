using MediatR;

namespace Geodata.Application.Geodata.Commands.AcceptCheckedGeoData;

internal class AcceptCheckedGeoData : IRequest
{
    public string Title { get; set; }
    public string Details { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }
}
