using Microsoft.AspNetCore.Mvc;
using tripPairAPI.Data;
using tripPairAPI.Interfaces;
using tripPairAPI.Models;

namespace tripPairAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ResortsController : Controller
{
    private readonly TripPairDbContext _db;
    private readonly IResortRepository _resortRepository;

    public ResortsController(TripPairDbContext db, IResortRepository resortRepository)
    {
        _db = db;
        _resortRepository = resortRepository;
    }
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Resort>))]
    public IActionResult GetAllResorts()
    {
        var resorts = _resortRepository.GetAllResorts().Result;
        return Ok(resorts);
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Resort>))]
    [Route("{searchTerm}")]
    public IActionResult GetResortsBySearch(string searchTerm)
    {
        var filteredResorts = _resortRepository.GetResortsBySearch(searchTerm).Result;
        return Ok(filteredResorts);
    }
    
    [HttpPost]
    [ProducesResponseType(200, Type = typeof(Resort))]
    public async Task<IActionResult> CreateResort(ResortDto newResort)
    {
        //TODO: replace with location repository
        var existingLocation = await _db.Locations.FindAsync(newResort.LocationId);

        //TODO: delete after integrated with frontend
        if (existingLocation == null) return NotFound();
        
        var resort = await _resortRepository.CreateResort(newResort);
        return Ok(resort);
    }

    [HttpDelete]
    [ProducesResponseType(200, Type = typeof(Resort))]
    [Route("{id:int}")]
    public async Task<IActionResult> DeleteResort([FromRoute] int id)
    {
        var deletedResort = await _resortRepository.DeleteResort(id);
        
        //TODO: delete after integrated with frontend
        if (deletedResort == null) return NotFound();

        return Ok(deletedResort);
    }
}