using AutoMapper;
using Geodata.Application.Common.Mappings;
using Geodata.Application.Geodata.Commands.DeleteAllGeodata;
using Geodata.Application.Geodata.Commands.DeleteGeodata;
using Geodata.Application.Geodata.Queries.GetGeodata;

namespace Geodata.Api.Models;

public class DeleteGeodataDto : IMapWith<DeleteAllGeodataCommand>
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<DeleteGeodataDto, DeleteAllGeodataCommand>()
            .ForMember(geoDataDto => geoDataDto.UserId,
                opt => opt.MapFrom(geoData => geoData.UserId));
    }
}
