using MediatR;

namespace Geodata.Application.Geodata.Commands.UpdateGeodata;

public class UpdateGeodataCommand : IRequest
{
    public Guid UserId { get; set; }
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Details { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }
    /// <summary>
    /// Checked by moderator
    /// </summary>
    public bool IsChecked { get; set; }
}
