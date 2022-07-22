using AutoMapper;
using tripPairAPI.Data;
using tripPairAPI.Models;

namespace tripPairAPI.Helper;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<ResortDto, Resort>();
    }
}