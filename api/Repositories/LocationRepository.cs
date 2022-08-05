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
        var locations = await _db.Locations.Include(l => l.LocationMonths).ThenInclude(lm => lm.Month).ToListAsync();
        return locations;
    }

    public async Task<Location> GetLocationById(int id)
    {
        var location = await _db.Locations.Include(l => l.LocationMonths).ThenInclude(lm => lm.Month).Where(l => l.Id == id).FirstOrDefaultAsync();
        return location;
    }

    public async Task<IEnumerable<Month>> GetLocationMonths(int locationId)
    {
        var locationMonths = await _db.LocationMonths.Where(lm => lm.LocationId == locationId).Select(lm => lm.Month).ToListAsync();
        return locationMonths;
    }
    
    public async Task<Location> UpdateLocation(int id, Location locationToUpdate)
    {
        //TODO: Missing mapping
        var existingLocation = await _db.Locations.FindAsync(id);
        if (existingLocation == null) return null;
        
        //Remove all previously assigned location months
        var existingLocationMonths = _db.LocationMonths.Where(lm => lm.LocationId == existingLocation.Id);
        foreach (var existingLocationMonth in existingLocationMonths)
        {
            _db.LocationMonths.Remove(existingLocationMonth);
        }

        existingLocation.Name = locationToUpdate.Name;
        existingLocation.GoodMonthsDescription = locationToUpdate.GoodMonthsDescription;
        existingLocation.LocationMonths = locationToUpdate.LocationMonths;
        
        _db.Locations.Update(existingLocation);
        await _db.SaveChangesAsync();
        return existingLocation;
    }

    public async Task<Location> CreateLocation(Location newLocation)
    {
        await _db.Locations.AddAsync(newLocation);
        await _db.SaveChangesAsync();
        return newLocation;
    }
    
    public async Task<Location> DeleteLocation(int id)
    {
        var locationToDelete = await _db.Locations.FindAsync(id);
        if (locationToDelete == null) return null;
        
        //Cleanup and remove location reference in n-m relationship table
        var locationMonthsToDelete = await _db.LocationMonths.Where(lm => lm.LocationId == locationToDelete.Id).ToListAsync();
        foreach (var locationMonth in locationMonthsToDelete)
        {
            _db.LocationMonths.Remove(locationMonth);
        }
        
        _db.Locations.Remove(locationToDelete);
        await _db.SaveChangesAsync();
        return locationToDelete;
    }

    public bool LocationExists(int id)
    {
        return _db.Locations.Any(l => l.Id == id);
    }
}