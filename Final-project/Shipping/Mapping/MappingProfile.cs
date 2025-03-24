using AutoMapper;
using Shipping.core.Models;
using Shipping.DTO;

namespace Shipping.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Governorate, GovernorateDTO>();
            CreateMap<City, CityDTO>();

        }
    }
}
