using TripPair.Api.Models;

namespace TripPair.Api.Interfaces;

public interface IResortRepository
{
    Task<IEnumerable<Resort>> GetAllResorts();
    Task<IEnumerable<Resort>> GetResortsBySearch(string searchTerm);
    Task<Resort?> GetResortById(int resortId);
    Task<Resort?> GetResortByName(string resortName, int resortLocationId);
    Task<Resort> CreateResort(Resort newResort);
    Task<Resort?> DeleteResort(int resortId);
}