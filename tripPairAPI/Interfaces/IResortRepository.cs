using tripPairAPI.Data;
using tripPairAPI.Models;

namespace tripPairAPI.Interfaces;

public interface IResortRepository
{
    Task<IEnumerable<Resort>> GetAllResorts();
    Task<IEnumerable<Resort>> GetResortsBySearch(string searchTerm);
    Task<Resort> GetResort(int resortId);
    Task<Resort> CreateResort(Resort newResort);
    Task<Resort> DeleteResort(int id);
    bool ResortExists(int id);
}