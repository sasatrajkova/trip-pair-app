using Microsoft.EntityFrameworkCore;
using tripPairAPI.Data;
using tripPairAPI.Interfaces;
using tripPairAPI.Models;

namespace tripPairAPI.Repositories;

public class ResortRepository : IResortRepository
{
    private readonly TripPairDbContext _db;
    
    public ResortRepository(TripPairDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Resort>> GetAllResorts()
    {
        return await _db.Resorts.Include(r => r.Location).ToListAsync();
    }

    public async Task<IEnumerable<Resort>> GetResortsBySearch(string searchTerm)
    {
        var filteredResorts = _db.Resorts.Include(r => r.Location).Where(r => r.Name.ToLower().Contains(searchTerm.ToLower())
                                                                              || r.Location.Name.ToLower().Contains(searchTerm.ToLower())
                                                                              || r.Climate.ToLower().Contains(searchTerm)).ToListAsync();
        return await filteredResorts;
    }

    public async Task<Resort> CreateResort(ResortDto newResort)
    {
        var resort = new Resort()
        {
            Name = newResort.Name,
            Climate = newResort.Climate,
            Image = newResort.Image,
            LocationId = newResort.LocationId
        };
        await _db.Resorts.AddAsync(resort);
        await _db.SaveChangesAsync();
        return resort;
    }

    public async Task<Resort> DeleteResort(int id)
    {
        var resortToDelete = await _db.Resorts.FindAsync(id);
        
        //TODO: delete after integrated with frontend
        if (resortToDelete == null) return null;

        _db.Resorts.Remove(resortToDelete);
        await _db.SaveChangesAsync();
        return resortToDelete;
    }
}