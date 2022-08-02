using AutoMapper;
using Microsoft.EntityFrameworkCore;
using tripPairAPI.Data;
using tripPairAPI.Interfaces;
using tripPairAPI.Models;

namespace tripPairAPI.Repositories;

public class LocationRepository : ILocationRepository
{
    private readonly TripPairDbContext _db;

    public LocationRepository(TripPairDbContext db)
    {
        _db = db;
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
    
    public async Task<Location> UpdateLocation(int id, Location locationToUpdate)
    {
        //TODO: Missing mapping
        var existingLocation = await _db.Locations.FindAsync(id);
        if (existingLocation == null) return null;

        var updatedLocation = locationToUpdate;
        existingLocation = updatedLocation;
        _db.Locations.Update(existingLocation);
        await _db.SaveChangesAsync();
        return existingLocation;
    }

    public async Task<Location> CreateLocation(Location locationToCreate)
    {
        await _db.Locations.AddAsync(locationToCreate);
        await _db.SaveChangesAsync();
        return locationToCreate;
    }
    
    public async Task<Location> DeleteLocation(int id)
    {
        var locationToRemove = await _db.Locations.FindAsync(id);
        if (locationToRemove == null) return null;
        
        var locationMonthsToRemove = await _db.LocationMonths.Where(lm => lm.LocationId == locationToRemove.Id).ToListAsync();
        _db.Locations.Remove(locationToRemove);
        foreach (var locationMonth in locationMonthsToRemove)
        {
            _db.LocationMonths.Remove(locationMonth);
        }
        await _db.SaveChangesAsync();
        return locationToRemove;
    }

    public bool LocationExists(int id)
    {
        return _db.Locations.Any(l => l.Id == id);
    }
}