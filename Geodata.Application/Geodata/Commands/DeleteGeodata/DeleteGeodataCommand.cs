using MediatR;

namespace Geodata.Application.Geodata.Commands.DeleteGeodata;

public class DeleteGeodataCommand : IRequest
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
}
