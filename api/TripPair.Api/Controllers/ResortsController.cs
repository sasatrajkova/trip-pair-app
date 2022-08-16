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
    private readonly IMapper _mapper;

    public ResortsController(IResortRepository resortRepository, IMapper mapper)
    {
        _resortRepository = resortRepository;
        _mapper = mapper;
    }
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<ResortDto>))]
    public async Task<OkObjectResult> GetAllResorts()
    {
        var resorts = _mapper.Map<List<ResortDto>>(await _resortRepository.GetAllResorts());
        return Ok(resorts);
    }

    [HttpGet]
    
    [ProducesResponseType(200, Type = typeof(IEnumerable<ResortDto>))]
    [Route("{searchTerm}")]
    public async Task<OkObjectResult> GetResortsBySearch(string searchTerm)
    {
        var filteredResorts =  _mapper.Map<IEnumerable<ResortDto>>(await _resortRepository.GetResortsBySearch(searchTerm));
        return Ok(filteredResorts);
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<ResortDto>))]
    [ProducesResponseType(404)]
    [Route("{id:int}")]
    public async Task<IActionResult> GetResort(int id)
    {
        var resort = _mapper.Map<ResortDto>(await _resortRepository.GetResortById(id));
        return resort != null ? Ok(resort) : NotFound();
    }
    
    [HttpPost]
    [ProducesResponseType(200, Type = typeof(ResortDto))]
    [ProducesResponseType(400)]
    [ProducesResponseType(422)]
    public async Task<IActionResult> CreateResort(ResortCreateDto resortToCreate)
    {
        //TODO: remove once frontend validation is implemented
        // var existingLocation = await _locationRepository.GetLocationById(resortToCreate.LocationId);
        // if (existingLocation == null)
        // {
        //     ModelState.AddModelError("", "Location does not exist");
        //     return StatusCode(422, ModelState);
        // }
        
        if (!ModelState.IsValid) return BadRequest();

        var existingResort = await _resortRepository.GetResortByName(resortToCreate.Name, resortToCreate.LocationId);
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
        var deletedResort = _mapper.Map<ResortDto>(await _resortRepository.DeleteResort(id));
        return deletedResort != null ? Ok(deletedResort) : NotFound();
    }
}