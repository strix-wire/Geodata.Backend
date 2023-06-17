using AutoMapper;
using Geodata.Application.Common.Mappings;
using Geodata.Domain;

namespace Geodata.Application.Geodata.Queries.GetGeodataList;

public class GeodataLookupDto : IMapWith<GeodataDomain>
{
    //Each list event should only have those fields
    //which the event list itself needs
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string? Details { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? EditDate { get; set; }
    /// <summary>
    /// Checked by moderator
    /// </summary>
    public bool IsChecked { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<GeodataDomain, GeodataLookupDto>()
            .ForMember(geoDataDto => geoDataDto.Id,
                opt => opt.MapFrom(geoData => geoData.Id))
            .ForMember(geoDataDto => geoDataDto.Title,
                opt => opt.MapFrom(geoData => geoData.Title))
            .ForMember(geoDataDto => geoDataDto.Details,
                opt => opt.MapFrom(geoData => geoData.Details))
            .ForMember(geoDataDto => geoDataDto.Latitude,
                opt => opt.MapFrom(geoData => geoData.Latitude))
            .ForMember(geoDataDto => geoDataDto.Longitude,
                opt => opt.MapFrom(geoData => geoData.Longitude))
            .ForMember(geoDataDto => geoDataDto.CreationDate,
                opt => opt.MapFrom(geoData => geoData.CreationDate))
            .ForMember(geoDataDto => geoDataDto.EditDate,
                opt => opt.MapFrom(geoData => geoData.EditDate))
            .ForMember(geoDataDto => geoDataDto.IsChecked,
                opt => opt.MapFrom(geoData => geoData.IsChecked));
    }
}
