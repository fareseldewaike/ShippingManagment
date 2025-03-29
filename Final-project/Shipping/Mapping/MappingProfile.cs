using AutoMapper;
using DTOs.DTO.Branch;
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

            CreateMap<Branch, BranchDto>().ReverseMap();
            CreateMap<Branch, AddBranchDto>().ReverseMap();

        }
    }
}
