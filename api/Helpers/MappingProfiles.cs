using AutoMapper;
using tripPairAPI.Data;
using tripPairAPI.Models;

namespace tripPairAPI.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<ResortDto, Resort>().ReverseMap();
        CreateMap<ResortCreateDto, Resort>();
        CreateMap<LocationDto, Location>().ReverseMap();
        CreateMap<LocationCreateDto, Location>();
        CreateMap<LocationMonth, LocationMonthDto>().ReverseMap();
        CreateMap<LocationMonthCreateDto, LocationMonth>();
        CreateMap<Month, MonthDto>();
    }
}