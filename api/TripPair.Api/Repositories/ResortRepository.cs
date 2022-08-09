using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TripPair.Api.Data;
using TripPair.Api.Interfaces;
using TripPair.Api.Models;

namespace TripPair.Api.Repositories;

public class ResortRepository : IResortRepository
{
    private readonly TripPairDbContext _db;
    private readonly IMapper _mapper;

    public ResortRepository(TripPairDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Resort>> GetAllResorts()
    {
        return await _db.Resorts.Include(r => r.Location).ThenInclude(l => l.LocationMonths).ThenInclude(lm => lm.Month).ToListAsync();
    }

    public async Task<IEnumerable<Resort>> GetResortsBySearch(string searchTerm)
    {
        var filteredResorts = _db.Resorts.Include(r => r.Location).ThenInclude(l => l.LocationMonths).ThenInclude(lm => lm.Month).Where(r => r.Name.ToLower().Contains(searchTerm.ToLower())
                                                                              || r.Location.Name.ToLower().Contains(searchTerm.ToLower())
                                                                              || r.Climate.ToLower().Contains(searchTerm)).ToListAsync();
        return await filteredResorts;
    }

    public async Task<Resort> GetResort(int resortId)
    {
        var resort = await _db.Resorts.Include(r => r.Location).ThenInclude(l => l.LocationMonths).ThenInclude(lm => lm.Month).Where(r => r.Id == resortId).FirstOrDefaultAsync();
        return resort;
    }

    public async Task<Resort> CreateResort(Resort newResort)
    {
        await _db.Resorts.AddAsync(newResort);
        await _db.SaveChangesAsync();
        return newResort;
    }

    public async Task<Resort> DeleteResort(int id)
    {
        var resortToDelete = await _db.Resorts.FindAsync(id);
        
        if (resortToDelete == null) return null;

        _db.Resorts.Remove(resortToDelete);
        await _db.SaveChangesAsync();
        return resortToDelete;
    }

    public bool ResortExists(int id)
    {
        return _db.Resorts.Any(r => r.Id == id);
    }
}