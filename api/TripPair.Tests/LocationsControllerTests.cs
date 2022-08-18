using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TripPair.Api.Controllers;
using TripPair.Api.Data;
using TripPair.Api.Helpers;
using TripPair.Api.Interfaces;
using TripPair.Api.Models;
using TripPair.Tests.Helpers;
using Xunit;

namespace TripPair.Tests;

public class LocationsControllerTests
{
    private static readonly MapperConfiguration MockMapper = new(config => config.AddProfile(new MappingProfiles()));
    private readonly Mock<ILocationRepository> _locationRepositoryStub = new();
    private readonly IMapper _mapper = MockMapper.CreateMapper();

    //Naming convention: UnitOfWork_StateUnderTest_ExpectedBehavior()

    [Fact]
    public async Task GetAllLocations_Always_ReturnsOkResponseWithAllLocationDtos()
    {
        //Arrange: prepare data
        var availableLocations = LocationsControllerHelper.ListOfLocations().ToList();

        _locationRepositoryStub
            .Setup(repo => repo.GetAllLocations())
            .ReturnsAsync(availableLocations);

        var locationController =
            new LocationsController(_locationRepositoryStub.Object, _mapper);

        var expectedResortDtosCount = availableLocations.Count;

        //Act: call the method
        var result = await locationController.GetAllLocations();
        var obj = result.Value as IEnumerable<LocationDto>;

        //Assert: compare expected result with actual
        obj?.Count().Should().Be(expectedResortDtosCount);
    }

    [Fact]
    public async Task GetLocation_WithExistingLocation_ReturnsOkResponseWithExpectedLocationDto()
    {
        //Arrange: prepare data
        var availableLocations = LocationsControllerHelper.ListOfLocations().ToList();

        _locationRepositoryStub
            .Setup(repo => repo.GetLocationById(It.IsAny<int>()))
            .ReturnsAsync((int i) => availableLocations.FirstOrDefault(l => l.Id == i));

        var locationsController =
            new LocationsController(_locationRepositoryStub.Object, _mapper);

        var expectedLocationDto = _mapper.Map<LocationDto>(availableLocations.FirstOrDefault(l => l.Id == 1));

        //Act: call the method
        var result = await locationsController.GetLocation(1) as OkObjectResult;
        var obj = result?.Value as LocationDto;

        //Assert: compare expected result with actual
        obj?.Should().BeEquivalentTo(expectedLocationDto);
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task GetLocation_WithNonExistingLocation_ReturnsNotFound()
    {
        //Arrange: prepare data
        var availableLocations = LocationsControllerHelper.ListOfLocations().ToList();

        _locationRepositoryStub
            .Setup(repo => repo.GetLocationById(It.IsAny<int>()))
            .ReturnsAsync((int i) => availableLocations.FirstOrDefault(l => l.Id == i));

        var locationsController =
            new LocationsController(_locationRepositoryStub.Object, _mapper);

        //Act: call the method
        var result = await locationsController.GetLocation(100);

        //Assert: compare expected result with actual
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task GetLocationMonths_WithExistingMonths_ReturnsOkResponseWithExpectedMonthDtos()
    {
        //Arrange: prepare data
        var availableLocationMonths = LocationsControllerHelper.ListOfLocationMonths();

        _locationRepositoryStub
            .Setup(repo => repo.GetLocationMonths(It.IsAny<int>()))
            .ReturnsAsync((int i) => availableLocationMonths.Where(lm => lm.LocationId == i).Select(lm => lm.Month));

        var locationsController =
            new LocationsController(_locationRepositoryStub.Object, _mapper);

        var expectedMonthDtos =
            _mapper.Map<List<MonthDto>>(availableLocationMonths.Where(lm => lm.LocationId == 1).Select(lm => lm.Month));

        //Act: call the method
        var result = await locationsController.GetLocationMonths(1) as OkObjectResult;
        var obj = result?.Value as List<MonthDto>;

        //Assert: compare expected result with actual
        obj?.Should().BeEquivalentTo(expectedMonthDtos);
        result.Should().BeOfType<OkObjectResult>();
    }

    //TODO: check validity
    [Fact]
    public async Task UpdateLocation_WithExistingLocation_ReturnsOkResponseWithExpectedLocationDto()
    {
        //Arrange: prepare data
        var existingLocation = LocationsControllerHelper.ListOfLocations().First(l => l.Id == 1);

        var locationToUpdate = new LocationCreateDto
        {
            Name = "location1",
            GoodMonthsDescription = "newDescription",
            LocationMonths = new List<LocationMonthCreateDto>()
        };

        _locationRepositoryStub
            .Setup(repo => repo.UpdateLocation(It.IsAny<int>(), It.IsAny<Location>()))
            .ReturnsAsync((int i, Location l) => new Location
            {
                Id = i, Name = l.Name, GoodMonthsDescription = l.GoodMonthsDescription,
                LocationMonths = _mapper.Map<List<LocationMonth>>(l.LocationMonths)
            });

        var locationsController =
            new LocationsController(_locationRepositoryStub.Object, _mapper);

        var expectedLocation = new Location
        {
            Id = existingLocation.Id, Name = locationToUpdate.Name,
            GoodMonthsDescription = locationToUpdate.GoodMonthsDescription,
            LocationMonths = _mapper.Map<List<LocationMonth>>(locationToUpdate.LocationMonths)
        };

        //Act: call the method
        var result = await locationsController.UpdateLocation(1, locationToUpdate) as OkObjectResult;
        var obj = result?.Value as LocationDto;

        //Assert: compare expected result with actual
        obj.Should().BeEquivalentTo(_mapper.Map<LocationDto>(expectedLocation));
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task UpdateLocation_WithInvalidRequest_ReturnsBadRequest()
    {
        //Arrange: prepare data
        var availableLocations = LocationsControllerHelper.ListOfLocations().ToList();

        _locationRepositoryStub
            .Setup(repo => repo.UpdateLocation(It.IsAny<int>(), It.IsAny<Location>()))
            .ReturnsAsync((int i, Location l) => availableLocations.FirstOrDefault(lo => lo.Id == i));

        var locationsController =
            new LocationsController(_locationRepositoryStub.Object, _mapper);

        locationsController.ModelState.AddModelError("", "Model state invalid");

        //Act: call the method
        var result = await locationsController.UpdateLocation(1, new LocationCreateDto());

        //Assert: compare expected result with actual
        result.Should().BeOfType<BadRequestResult>();
    }

    [Fact]
    public async Task UpdateLocation_WithNonExistingLocation_ReturnsNotFound()
    {
        //Arrange: prepare data
        var availableLocations = LocationsControllerHelper.ListOfLocations().ToList();

        _locationRepositoryStub
            .Setup(repo => repo.UpdateLocation(It.IsAny<int>(), It.IsAny<Location>()))
            .ReturnsAsync((int i, Location l) => availableLocations.FirstOrDefault(lo => lo.Id == i));

        var locationsController =
            new LocationsController(_locationRepositoryStub.Object, _mapper);

        //Act: call the method
        var result = await locationsController.UpdateLocation(100, new LocationCreateDto());

        //Assert: compare expected result with actual
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task CreateLocation_WithCreatedLocation_ReturnsOkResponseWithCreatedLocationDto()
    {
        //Arrange: prepare data
        var locationToCreate = new LocationCreateDto
        {
            Name = "location1",
            GoodMonthsDescription = "description"
        };

        _locationRepositoryStub
            .Setup(repo => repo.CreateLocation(It.IsAny<Location>()))
            .ReturnsAsync((Location l) => new Location
            {
                Id = new Random().Next(), Name = l.Name, GoodMonthsDescription = l.GoodMonthsDescription,
                LocationMonths = l.LocationMonths
            });

        var locationsController =
            new LocationsController(_locationRepositoryStub.Object, _mapper);

        var expectedLocationDto = _mapper.Map<LocationDto>(_mapper.Map<Location>(locationToCreate));

        //Act: call the method
        var result = await locationsController.CreateLocation(locationToCreate) as OkObjectResult;
        var obj = result?.Value as LocationDto;

        //Assert: compare expected result with actual
        obj.Should().BeEquivalentTo(expectedLocationDto);
        result.Should().BeOfType<OkObjectResult>();
    }

    //TODO: check necessity
    [Fact]
    public async Task CreateLocation_WithInvalidRequest_ReturnsBadRequest()
    {
        //Arrange: prepare data
        _locationRepositoryStub
            .Setup(repo => repo.CreateLocation(It.IsAny<Location>()))
            .ReturnsAsync((Location l) => new Location
            {
                Id = l.Id, Name = l.Name, GoodMonthsDescription = l.GoodMonthsDescription,
                LocationMonths = l.LocationMonths
            });

        var locationsController =
            new LocationsController(_locationRepositoryStub.Object, _mapper);

        locationsController.ModelState.AddModelError("", "Model state invalid");

        //Act: call the method
        var result = await locationsController.CreateLocation(new LocationCreateDto());

        //Assert: compare expected result with actual
        result.Should().BeOfType<BadRequestResult>();
    }

    //TODO: Check necessity: duplication with CreateLocation_WithInvalidRequest_ReturnsBadRequest()
    [Fact]
    public async Task CreateLocation_WithExistingLocation_ReturnsLocationAlreadyExists()
    {
        //Arrange: prepare data
        _locationRepositoryStub
            .Setup(repo => repo.CreateLocation(It.IsAny<Location>()))
            .ReturnsAsync((Location l) => new Location
            {
                Id = l.Id, Name = l.Name, GoodMonthsDescription = l.GoodMonthsDescription,
                LocationMonths = l.LocationMonths
            });

        var locationsController =
            new LocationsController(_locationRepositoryStub.Object, _mapper);

        locationsController.ModelState.AddModelError("", "Location already exists");

        //Act: call the method
        var result = await locationsController.CreateLocation(new LocationCreateDto());

        //Assert: compare expected result with actual
        result.Should().BeOfType<BadRequestResult>();
    }


    [Fact]
    public async Task DeleteLocation_WithExistingLocation_ReturnsOkResponseWithDeletedLocationDto()
    {
        //Arrange: prepare data
        var availableLocations = LocationsControllerHelper.ListOfLocations().ToList();

        _locationRepositoryStub
            .Setup(repo => repo.DeleteLocation(It.IsAny<int>()))
            .ReturnsAsync((int i) => availableLocations.FirstOrDefault(l => l.Id == i));

        var locationsController =
            new LocationsController(_locationRepositoryStub.Object, _mapper);

        var expectedLocationDto = _mapper.Map<LocationDto>(availableLocations.FirstOrDefault(l => l.Id == 1));

        //Act: call the method
        var result = await locationsController.DeleteLocation(1) as OkObjectResult;
        var obj = result?.Value as LocationDto;

        //Assert: compare expected result with actual
        obj.Should().BeEquivalentTo(expectedLocationDto);
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task DeleteLocation_WithNonExistingLocation_ReturnsNotFound()
    {
        //Arrange: prepare data
        var availableLocations = LocationsControllerHelper.ListOfLocations().ToList();

        _locationRepositoryStub
            .Setup(repo => repo.DeleteLocation(It.IsAny<int>()))
            .ReturnsAsync((int i) => availableLocations.FirstOrDefault(l => l.Id == i));

        var locationsController =
            new LocationsController(_locationRepositoryStub.Object, _mapper);

        //Act: call the method
        var result = await locationsController.DeleteLocation(100);

        //Assert: compare expected result with actual
        result.Should().BeOfType<NotFoundResult>();
    }
}