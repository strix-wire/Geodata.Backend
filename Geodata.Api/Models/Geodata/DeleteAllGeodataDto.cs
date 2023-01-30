using AutoMapper;
using Geodata.Application.Common.Mappings;
using Geodata.Application.Geodata.Commands.DeleteAllGeodata;

namespace Geodata.Api.Models.Geodata
{
    public class DeleteAllGeodataDto : IMapWith<DeleteAllGeodataCommand>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<DeleteAllGeodataDto, DeleteAllGeodataCommand>()
                .ForMember(geoDataDto => geoDataDto.UserId,
                    opt => opt.MapFrom(geoData => geoData.UserId));
        }
    }
}
