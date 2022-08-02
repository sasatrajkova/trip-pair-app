using tripPairAPI.Data;
using tripPairAPI.Models;

namespace tripPairAPI.Interfaces;

public interface ILocationRepository
{
    Task<IEnumerable<Location>> GetAllLocations();
    Task<Location> GetLocationById(int id);
    Task<IEnumerable<Month>> GetLocationMonths(int locationId);
    Task<Location> UpdateLocation(int id, Location locationToUpdate);
    Task<Location> CreateLocation(Location locationToCreate);
    Task<Location> DeleteLocation(int id);
    bool LocationExists(int id);
}