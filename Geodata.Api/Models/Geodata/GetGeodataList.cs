using AutoMapper;
using Geodata.Application.Common.Mappings;
using Geodata.Application.Geodata.Queries.GetGeodata;
using Geodata.Application.Geodata.Queries.GetGeodataList;

namespace Geodata.Api.Models.Geodata;

public class GetGeodataList : IMapWith<GeodataListQuery>
{
    public Guid UserId { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<GetGeodataList, GeodataListQuery>()
            .ForMember(geoDataDto => geoDataDto.UserId,
                opt => opt.MapFrom(geoData => geoData.UserId));
    }
}
