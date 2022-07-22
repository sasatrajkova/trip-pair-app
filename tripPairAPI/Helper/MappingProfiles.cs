using AutoMapper;
using tripPairAPI.Data;
using tripPairAPI.Models;

namespace tripPairAPI.Helper;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<ResortDto, Resort>();
        CreateMap<LocationDto, Location>();
        CreateMap<Location, LocationDto>();
        CreateMap<Month, MonthDto>();
        CreateMap<LocationMonth, LocationMonthDto>();
        CreateMap<LocationMonthDto, LocationMonth>();

    }
}