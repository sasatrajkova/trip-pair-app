using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TripPair.Api.Data;
using TripPair.Api.Interfaces;
using TripPair.Api.Models;

namespace TripPair.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ResortsController : Controller
{
    private readonly IResortRepository _resortRepository;
    private readonly ILocationRepository _locationRepository;
    private readonly IMapper _mapper;

    public ResortsController(IResortRepository resortRepository, ILocationRepository locationRepository, IMapper mapper)
    {
        _resortRepository = resortRepository;
        _locationRepository = locationRepository;
        _mapper = mapper;
    }
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<ResortDto>))]
    public IActionResult GetAllResorts()
    {
        var resorts = _mapper.Map<List<ResortDto>>(_resortRepository.GetAllResorts().Result);
        return Ok(resorts);
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<ResortDto>))]
    [Route("{searchTerm}")]
    public IActionResult GetResortsBySearch(string searchTerm)
    {
        var filteredResorts =  _mapper.Map<List<ResortDto>>(_resortRepository.GetResortsBySearch(searchTerm).Result);
        return Ok(filteredResorts);
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<ResortDto>))]
    [ProducesResponseType(404)]
    [Route("{id:int}")]
    public async Task<IActionResult> GetResort(int id)
    {
        if (!_resortRepository.ResortExists(id)) return NotFound();
        
        var resort = _mapper.Map<ResortDto>(await _resortRepository.GetResort(id));
        return Ok(resort);
    }
    
    [HttpPost]
    [ProducesResponseType(200, Type = typeof(ResortDto))]
    [ProducesResponseType(400)]
    [ProducesResponseType(422)]
    public async Task<IActionResult> CreateResort(ResortCreateDto resortToCreate)
    {
        //Can be removed once frontend validation is implemented
        var existingLocation = await _locationRepository.GetLocationById(resortToCreate.LocationId);
        if (existingLocation == null)
        {
            ModelState.AddModelError("", "Location does not exist");
            return StatusCode(422, ModelState);
        }
        
        if (!ModelState.IsValid) return BadRequest();
        
        var existingResort = _resortRepository.GetAllResorts().Result
            .FirstOrDefault(r => r.Name.ToLower().Trim() == resortToCreate.Name.ToLower().Trim() && r.LocationId == resortToCreate.LocationId);
        if (existingResort != null)
        {
            ModelState.AddModelError("", "Resort already exists");
            return StatusCode(422, ModelState);
        }

        var createdResort = _mapper.Map<ResortDto>(await _resortRepository.CreateResort(_mapper.Map<Resort>(resortToCreate)));
        return Ok(createdResort);
    }

    [HttpDelete]
    [ProducesResponseType(200, Type = typeof(ResortDto))]
    [ProducesResponseType(404)]
    [Route("{id:int}")]
    public async Task<IActionResult> DeleteResort([FromRoute] int id)
    {
        if (!_resortRepository.ResortExists(id)) return NotFound();
        
        var deletedResort = _mapper.Map<ResortDto>(await _resortRepository.DeleteResort(id));
        return Ok(deletedResort);
    }
}