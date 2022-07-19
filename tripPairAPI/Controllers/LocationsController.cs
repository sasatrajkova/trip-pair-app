using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tripPairAPI.Data;

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
}