using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using tripPairAPI.Data;
using tripPairAPI.Interfaces;
using tripPairAPI.Models;

namespace tripPairAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LocationsController : Controller
{
    private readonly ILocationRepository _locationRepository;

    public LocationsController( ILocationRepository locationRepository, IMapper mapper)
    {
        _locationRepository = locationRepository;
    }
    
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Location>))]
    public async Task<IActionResult> GetAllLocations()
    {
        return Ok(await _locationRepository.GetAllLocations());
    }
    
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Location>))]
    [ProducesResponseType(404)]
    [Route("{id:int}")]
    public async Task<IActionResult> GetLocation([FromRoute] int id)
    {
        if (!_locationRepository.LocationExists(id)) return NotFound(); 
        return Ok(await _locationRepository.GetLocationById(id));
    }
    
    [HttpPut]
    [ProducesResponseType(200, Type = typeof(Location))]
    [ProducesResponseType(404)]
    [Route("{id:int}")]
    public async Task<IActionResult> UpdateLocation([FromRoute] int id, LocationDto locationToUpdate)
    {
        if (!_locationRepository.LocationExists(id)) return NotFound();
        var updatedLocation = _locationRepository.UpdateLocation(id, locationToUpdate);
        return Ok(await updatedLocation);
    }
    
    [HttpPost]
    [ProducesResponseType(200, Type = typeof(Location))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> CreateLocation(LocationDto locationToCreate)
    {
        if (!ModelState.IsValid) return BadRequest();
        var newLocation = await _locationRepository.CreateLocation(locationToCreate);
        return Ok(newLocation);
    }

    [HttpDelete]
    [ProducesResponseType(200, Type = typeof(Location))]
    [ProducesResponseType(404)]
    [Route("{id:int}")]
    public async Task<IActionResult> DeleteLocation([FromRoute] int id)
    {
        if (!_locationRepository.LocationExists(id)) return NotFound();
        return Ok(await _locationRepository.DeleteLocation(id));
    }
    
}