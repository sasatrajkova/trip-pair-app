using tripPairAPI.Data;
using tripPairAPI.Models;

namespace tripPairAPI.Interfaces;

public interface IResortRepository
{
    Task<IEnumerable<Resort>> GetAllResorts();
    Task<IEnumerable<Resort>> GetResortsBySearch(string searchTerm);
    Task<Resort> GetResort(int id);
    Task<Resort> CreateResort(ResortDto newResort);
    Task<Resort> DeleteResort(int id);
    bool ResortExists(int id);
}