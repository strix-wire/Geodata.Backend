using AutoMapper;
using Geodata.Application.Common.Mappings;
using Geodata.Application.Geodata.Commands.UpdateGeodata;

namespace Geodata.Api.Models;

public class UpdateGeodataDto : IMapWith<UpdateGeodataCommand>
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

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateGeodataDto, UpdateGeodataCommand>()
            .ForMember(geoDataDto => geoDataDto.Id,
                opt => opt.MapFrom(geoData => geoData.Id))
            .ForMember(geoDataDto => geoDataDto.UserId,
                opt => opt.MapFrom(geoData => geoData.UserId))
            .ForMember(geoDataDto => geoDataDto.Title,
                opt => opt.MapFrom(geoData => geoData.Title))
            .ForMember(geoDataDto => geoDataDto.Details,
                opt => opt.MapFrom(geoData => geoData.Details))
            .ForMember(geoDataDto => geoDataDto.Latitude,
                opt => opt.MapFrom(geoData => geoData.Latitude))
            .ForMember(geoDataDto => geoDataDto.Longitude,
                opt => opt.MapFrom(geoData => geoData.Longitude))
            .ForMember(geoDataDto => geoDataDto.IsChecked,
                opt => opt.MapFrom(geoData => geoData.IsChecked));
    }
}
