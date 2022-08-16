using AutoMapper;
using TripPair.Api.Data;
using TripPair.Api.Models;

namespace TripPair.Api.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Resort, ResortDto>();
        CreateMap<ResortCreateDto, Resort>();
        CreateMap<Location, LocationDto>();
        CreateMap<LocationCreateDto, Location>();
        CreateMap<LocationMonth, LocationMonthDto>();
        CreateMap<LocationMonthCreateDto, LocationMonth>();
        CreateMap<Month, MonthDto>();
    }
}