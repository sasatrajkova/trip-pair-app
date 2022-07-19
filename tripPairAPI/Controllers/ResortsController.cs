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
        var filteredResorts = _db.Resorts.Where(r => r.Name.ToLower().Contains(searchTerm.ToLower())
                                                     || r.Location.ToLower().Contains(searchTerm.ToLower())
                                                     || r.Climate.ToLower().Contains(searchTerm)).ToListAsync();
        return Ok(await filteredResorts);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateResort(ResortDto newResort)
    {
        var resort = new Resort()
        {
            Name = newResort.Name,
            Climate = newResort.Climate,
            Image = newResort.Image,
            Location = newResort.Location
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