using AutoMapper;
using Geodata.Application.Common.Mappings;
using Geodata.Application.Geodata.Commands.DeleteGeodata;
using Geodata.Application.Geodata.Queries.GetGeodata;

namespace Geodata.Api.Models;

public class DeleteGeodataDto : IMapWith<DeleteGeodataCommand>
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<DeleteGeodataDto, DeleteGeodataCommand>()
            .ForMember(geoDataDto => geoDataDto.Id,
                opt => opt.MapFrom(geoData => geoData.Id))
            .ForMember(geoDataDto => geoDataDto.UserId,
                opt => opt.MapFrom(geoData => geoData.UserId));
    }
}
