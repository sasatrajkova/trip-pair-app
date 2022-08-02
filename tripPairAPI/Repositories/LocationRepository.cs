using AutoMapper;
using Microsoft.EntityFrameworkCore;
using tripPairAPI.Data;
using tripPairAPI.Interfaces;
using tripPairAPI.Models;

namespace tripPairAPI.Repositories;

public class LocationRepository : ILocationRepository
{
    private readonly TripPairDbContext _db;
    private readonly IMapper _mapper;

    public LocationRepository(TripPairDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }
    public async Task<IEnumerable<Location>> GetAllLocations()
    {
        return await _db.Locations.Include(l => l.LocationMonths).ThenInclude(lm => lm.Month).ToListAsync();
    }

    public async Task<Location> GetLocationById(int id)
    {
        var location = await _db.Locations.Include(l => l.LocationMonths).ThenInclude(lm => lm.Month).Where(l => l.Id == id).FirstOrDefaultAsync();
        return location;
    }

    public async Task<IEnumerable<Month>> GetLocationMonths(int locationId)
    {
        return await _db.LocationMonths.Where(lm => lm.LocationId == locationId).Select(lm => lm.Month).ToListAsync();
    }
    
    public async Task<Location> UpdateLocation(int id, LocationCreateDto locationToUpdate)
    {
        //TODO: Missing mapping
        var updatedLocation = await _db.Locations.FindAsync(id);
        if (updatedLocation == null) return null;
        
        updatedLocation.Name = locationToUpdate.Name;
        updatedLocation.GoodMonthsDescription = locationToUpdate.GoodMonthsDescription;
        
        await _db.SaveChangesAsync();
        return updatedLocation;
    }

    public async Task<Location> CreateLocation(LocationCreateDto locationToCreate)
    {
        //TODO: Missing mapping and addition to table LocationMonths
        var newLocation = _mapper.Map<Location>(locationToCreate);
        await _db.Locations.AddAsync(newLocation);
        await _db.SaveChangesAsync();
        return newLocation;
    }
    
    public async Task<Location> DeleteLocation(int id)
    {
        var locationToRemove = await _db.Locations.FindAsync(id);
        if (locationToRemove == null) return null;
        
        _db.Locations.Remove(locationToRemove);
        await _db.SaveChangesAsync();
        return locationToRemove;
    }

    public bool LocationExists(int id)
    {
        return _db.Locations.Any(l => l.Id == id);
    }
}