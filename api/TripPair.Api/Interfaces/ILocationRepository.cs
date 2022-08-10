using TripPair.Api.Models;

namespace TripPair.Api.Interfaces;

public interface ILocationRepository
{
    Task<IEnumerable<Location>> GetAllLocations();
    Task<Location?> GetLocationById(int locationId);
    Task<Location?> GetLocationByName(string locationName);
    Task<IEnumerable<Month?>> GetLocationMonths(int locationId);
    Task<Location?> UpdateLocation(int id, Location locationToUpdate);
    Task<Location> CreateLocation(Location newLocation);
    Task<Location?> DeleteLocation(int locationId);
}