using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tripPairAPI.Data;
using tripPairAPI.Models;

namespace tripPairAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LocationsController : Controller
{
    private readonly TripPairDbContext _db;

    public LocationsController(TripPairDbContext db)
    {
        _db = db;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllLocations()
    {
        return Ok(await _db.Locations.ToListAsync());
    }
    
    [HttpGet]
    [Route("{name}")]
    public async Task<IActionResult> GetLocationByName([FromRoute] string name)
    {
        var location = _db.Locations.Where(l => l.Name.ToLower().Contains(name.ToLower())).FirstOrDefaultAsync();
        return Ok(await location);
    }
    
    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> UpdateLocation([FromRoute] int id, LocationDto updatedLocation)
    {
        var existingLocation = await _db.Locations.FindAsync(id);
        if (existingLocation == null)
        {
            return NotFound();
        }

        existingLocation.Name = updatedLocation.Name;
        existingLocation.GoodMonthsDescription = updatedLocation.GoodMonthsDescription;
        
        await _db.SaveChangesAsync();
        return Ok(existingLocation);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateLocation(LocationDto newLocation)
    {
        var location = new Location()
        {
            Name = newLocation.Name,
            GoodMonthsDescription = newLocation.GoodMonthsDescription
            
        };
        await _db.Locations.AddAsync(location);
        await _db.SaveChangesAsync();
        return Ok(location);
    }
}