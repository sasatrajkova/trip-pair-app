using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TripPair.Api.Data;
using TripPair.Api.Interfaces;
using TripPair.Api.Models;

namespace TripPair.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LocationsController : Controller
{
    private readonly ILocationRepository _locationRepository;
    private readonly IMapper _mapper;

    public LocationsController(ILocationRepository locationRepository, IMapper mapper)
    {
        _locationRepository = locationRepository;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<LocationDto>))]
    public async Task<IActionResult> GetAllLocations()
    {
        var locations = _mapper.Map<List<LocationDto>>(await _locationRepository.GetAllLocations());
        return Ok(locations);
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<LocationDto>))]
    [ProducesResponseType(404)]
    [Route("{id:int}")]
    public async Task<IActionResult> GetLocation([FromRoute] int id)
    {
        var location = _mapper.Map<LocationDto>(await _locationRepository.GetLocationById(id));
        return location != null ? Ok(location) : NotFound();
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<MonthDto>))]
    [ProducesResponseType(404)]
    [Route("{locationId:int}/Months")]
    public async Task<IActionResult> GetLocationMonths([FromRoute] int locationId)
    {
        var locationMonths = _mapper.Map<List<MonthDto>>(await _locationRepository.GetLocationMonths(locationId));
        return locationMonths != null ? Ok(locationMonths) : NotFound();
    }

    [HttpPut]
    [ProducesResponseType(200, Type = typeof(LocationDto))]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    [Route("{id:int}")]
    public async Task<IActionResult> UpdateLocation([FromRoute] int id, LocationCreateDto locationToUpdate)
    {
        if (!ModelState.IsValid) return BadRequest();

        var updatedLocation =
            _mapper.Map<LocationDto>(
                await _locationRepository.UpdateLocation(id, _mapper.Map<Location>(locationToUpdate)));
        return updatedLocation != null ? Ok(updatedLocation) : NotFound();
    }

    [HttpPost]
    [ProducesResponseType(200, Type = typeof(LocationDto))]
    [ProducesResponseType(400)]
    [ProducesResponseType(422)]
    public async Task<IActionResult> CreateLocation(LocationCreateDto locationToCreate)
    {
        if (!ModelState.IsValid) return BadRequest();

        var existingLocation = await _locationRepository.GetLocationByName(locationToCreate.Name);
        if (existingLocation != null)
        {
            ModelState.AddModelError("", "Location already exists");
            return StatusCode(422, ModelState);
        }

        var createdLocation =
            _mapper.Map<LocationDto>(await _locationRepository.CreateLocation(_mapper.Map<Location>(locationToCreate)));
        return Ok(createdLocation);
    }

    [HttpDelete]
    [ProducesResponseType(200, Type = typeof(LocationDto))]
    [ProducesResponseType(404)]
    [Route("{id:int}")]
    public async Task<IActionResult> DeleteLocation([FromRoute] int id)
    {
        var deletedLocation = _mapper.Map<LocationDto>(await _locationRepository.DeleteLocation(id));
        return deletedLocation != null ? Ok(deletedLocation) : NotFound();
    }
}