using AutoMapper;
using NZWalksAPI.Models.Domein;
using NZWalksAPI.Models.DTO;

namespace NZWalksAPI.Mapping
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() 
        {
            CreateMap<Region, RegionDto>();
            CreateMap<CreateRegionDto, Region>().ReverseMap();

            CreateMap<AddWalksDto, Walk>().ReverseMap();
            CreateMap<Walk, WalkDto>().ReverseMap();
        }
    }
}
