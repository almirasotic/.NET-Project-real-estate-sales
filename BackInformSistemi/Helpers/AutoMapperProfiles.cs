using AutoMapper;
using BackInformSistemi.Controllers;
using BackInformSistemi.Dtos;
using BackInformSistemi.Models;

namespace BackInformSistemi.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<City, CityDto>().ReverseMap();
            CreateMap<City, CityUpdateDto>().ReverseMap();
            CreateMap<PropertyDto, Property>();

            CreateMap<Property, PropertyDto>();
            //CreateMap<CityDto, City>();
            CreateMap<Property, PropertyListDto>()
                .ForMember(d => d.City, opt => opt.MapFrom(src => src.City.Name))

                .ForMember(d => d.Country, opt => opt.MapFrom(src => src.City.Country))

                .ForMember(d => d.PropertyType, opt => opt.MapFrom(src => src.PropertyType.Name))

                .ForMember(d => d.FurnishingType, opt => opt.MapFrom(src => src.FurnishingType.Name));

            CreateMap<Property, PropertyDetailDto>()
       .ForMember(d => d.CityId, opt => opt.MapFrom(src => src.City.Id))
      // .ForMember(d => d.CityName, opt => opt.MapFrom(src => src.City.Name))  // Mapiraj u string CityName
       .ForMember(d => d.Country, opt => opt.MapFrom(src => src.City.Country))
       .ForMember(d => d.PropertyType, opt => opt.MapFrom(src => src.PropertyType.Name))
       .ForMember(d => d.FurnishingType, opt => opt.MapFrom(src => src.FurnishingType.Name));

            CreateMap<PropertyType, KeyValuePairDto>();

            CreateMap<Property, KeyValuePairDto>().ReverseMap();
            CreateMap<FurnishingType, KeyValuePairDto>();
        }

    }
}
