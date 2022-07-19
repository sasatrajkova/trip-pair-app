using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tripPairAPI.Data;
using tripPairAPI.Models;

namespace tripPairAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ResortsController : Controller
{
    private readonly TripPairDbContext _db;

    public ResortsController(TripPairDbContext db)
    {
        _db = db;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllResorts()
    {
        return Ok(await _db.Resorts.ToListAsync());
    }

    [HttpGet]
    [Route("{searchTerm}")]
    public async Task<IActionResult> GetResortBySearch(string searchTerm)
    {
        var filteredResorts = _db.Resorts.Include(r => r.Location).Where(r => r.Name.ToLower().Contains(searchTerm.ToLower())
                                                     || r.Location.Name.ToLower().Contains(searchTerm.ToLower())
                                                     || r.Climate.ToLower().Contains(searchTerm)).ToListAsync();
        return Ok(await filteredResorts);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateResort(ResortDto newResort)
    {
        var existingLocation = await _db.Locations.FindAsync(newResort.LocationId);

        if (existingLocation == null)
        {
            return NotFound();
        }
        var resort = new Resort()
        {
            Name = newResort.Name,
            Climate = newResort.Climate,
            Image = newResort.Image,
            LocationId = newResort.LocationId
        };
        await _db.Resorts.AddAsync(resort);
        await _db.SaveChangesAsync();
        return Ok(resort);
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> DeleteResort([FromRoute] int id)
    {
        var existingResort = await _db.Resorts.FindAsync(id);
        if (existingResort == null) return NotFound();

        _db.Resorts.Remove(existingResort);
        await _db.SaveChangesAsync();
        
        return Ok();
    }
}