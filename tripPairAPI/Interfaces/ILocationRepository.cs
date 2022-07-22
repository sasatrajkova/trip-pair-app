using tripPairAPI.Data;
using tripPairAPI.Models;

namespace tripPairAPI.Interfaces;

public interface ILocationRepository
{
    Task<IEnumerable<Location>> GetAllLocations();
    Task<Location> GetLocationByName(string name);
    Task<Location> UpdateLocation(int id);
    Task<Location> CreateLocation(LocationDto newLocation);
    bool LocationExists(int id);
}