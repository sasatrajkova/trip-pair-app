using AutoMapper;
using tripPairAPI.Data;
using tripPairAPI.Models;

namespace tripPairAPI.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<ResortDto, Resort>();
        CreateMap<Resort, ResortDto>();
        CreateMap<ResortCreateDto, Resort>();
        CreateMap<LocationDto, Location>();
        CreateMap<Location, LocationDto>();
        CreateMap<LocationCreateDto, Location>();
        CreateMap<LocationMonth, LocationMonthDto>();
        CreateMap<LocationMonthDto, LocationMonth>();
        CreateMap<LocationMonthCreateDto, LocationMonth>();
        CreateMap<Month, MonthDto>();
    }
}