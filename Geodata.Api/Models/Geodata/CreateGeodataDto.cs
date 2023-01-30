using AutoMapper;
using Geodata.Application.Common.Mappings;
using Geodata.Application.Geodata.Commands.CreateGeodata;

namespace Geodata.Api.Models.Geodata;

public class CreateGeodataDto : IMapWith<CreateGeodataCommand>
{
    public Guid UserId { get; set; }
    public string Title { get; set; }
    public string? Details { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateGeodataDto, CreateGeodataCommand>()
            .ForMember(geoDataDto => geoDataDto.Title,
                opt => opt.MapFrom(geoData => geoData.Title))
            .ForMember(geoDataDto => geoDataDto.Details,
                opt => opt.MapFrom(geoData => geoData.Details))
            .ForMember(geoDataDto => geoDataDto.Latitude,
                opt => opt.MapFrom(geoData => geoData.Latitude))
            .ForMember(geoDataDto => geoDataDto.Longitude,
                opt => opt.MapFrom(geoData => geoData.Longitude))
            .ForMember(geoDataDto => geoDataDto.UserId,
                opt => opt.MapFrom(geoData => geoData.UserId));
    }
}
