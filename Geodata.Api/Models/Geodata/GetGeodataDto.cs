using AutoMapper;
using Geodata.Application.Common.Mappings;
using Geodata.Application.Geodata.Commands.CreateGeodata;
using Geodata.Application.Geodata.Queries.GetGeodata;

namespace Geodata.Api.Models.Geodata;

public class GetGeodataDto : IMapWith<GeodataDetailsQuery>
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<GetGeodataDto, GeodataDetailsQuery>()
            .ForMember(geoDataDto => geoDataDto.Id,
                opt => opt.MapFrom(geoData => geoData.Id))
            .ForMember(geoDataDto => geoDataDto.UserId,
                opt => opt.MapFrom(geoData => geoData.UserId));
    }
}
