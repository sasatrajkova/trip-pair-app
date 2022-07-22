using AutoMapper;
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
    private readonly IMapper _mapper;

    public ResortsController(TripPairDbContext db, IResortRepository resortRepository, IMapper mapper)
    {
        _db = db;
        _resortRepository = resortRepository;
        _mapper = mapper;
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

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Resort>))]
    [ProducesResponseType(404)]
    [Route("{id:int}")]
    public async Task<IActionResult> GetResort(int id)
    {
        if (!_resortRepository.ResortExists(id)) return NotFound();
        
        var resort = await _resortRepository.GetResort(id);
        return Ok(resort);
    }
    
    [HttpPost]
    [ProducesResponseType(200, Type = typeof(Resort))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> CreateResort(ResortDto newResort)
    {
        var existingLocation = await _db.Locations.FindAsync(newResort.LocationId);

        if (existingLocation == null) return NotFound();
        if (!ModelState.IsValid) return BadRequest();
        
        var resort = await _resortRepository.CreateResort(newResort);
        return Ok(resort);
    }

    [HttpDelete]
    [ProducesResponseType(200, Type = typeof(Resort))]
    [ProducesResponseType(404)]
    [Route("{id:int}")]
    public async Task<IActionResult> DeleteResort([FromRoute] int id)
    {
        if (!_resortRepository.ResortExists(id)) return NotFound();
        
        var deletedResort = await _resortRepository.DeleteResort(id);
        return Ok(deletedResort);
    }
}