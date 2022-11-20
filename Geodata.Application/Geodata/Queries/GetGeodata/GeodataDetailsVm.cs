using AutoMapper;
using Geodata.Domain;

namespace Geodata.Application.Geodata.Queries.GetGeodata;

internal class GeodataDetailsVm : IMapWith<GeodataEntity>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string? Details { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? EditDate { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }
    /// <summary>
    /// Checked by moderator
    /// </summary>
    public bool IsChecked { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<GeodataDomain, GeodataDetailsVm>()
            .ForMember(geoDataVm => geoDataVm.Title,
                opt => opt.MapFrom(geoData => geoData.Title))
            .ForMember(geoDataVm => geoDataVm.Details,
                opt => opt.MapFrom(geoData => geoData.Details))
            .ForMember(geoDataVm => geoDataVm.Id,
                opt => opt.MapFrom(geoData => geoData.Id))
            .ForMember(geoDataVm => geoDataVm.CreationDate,
                opt => opt.MapFrom(geoData => geoData.CreationDate))
            .ForMember(geoDataVm => geoDataVm.EditDate,
                opt => opt.MapFrom(geoData => geoData.EditDate))
            .ForMember(geoDataDto => geoDataDto.IsChecked,
                opt => opt.MapFrom(geoData => geoData.IsChecked));
    }
}
