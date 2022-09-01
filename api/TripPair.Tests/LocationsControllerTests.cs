using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using TripPair.Api.Controllers;
using TripPair.Api.Data;
using TripPair.Api.Helpers;
using TripPair.Api.Interfaces;
using TripPair.Api.Models;
using Xunit;

namespace TripPair.Tests;

public class LocationsControllerTests
{
    private static readonly MapperConfiguration MockMapper = new(config => config.AddProfile(new MappingProfiles()));
    private readonly Mock<ILocationRepository> _locationRepositoryStub = new();
    private readonly IMapper _mapper = MockMapper.CreateMapper();

    //Naming convention: UnitOfWork_StateUnderTest_ExpectedBehavior()

    [Fact]
    public async Task GetAllLocations_Always_CallsRepoFunctionOnceAndReturnsOk()
    {
        //Arrange: prepare data
        var locationController =
            new LocationsController(_locationRepositoryStub.Object, _mapper);

        //Act: call the method
        var result = await locationController.GetAllLocations();

        //Assert: compare expected result with actual
        _locationRepositoryStub.Verify(repo => repo.GetAllLocations(), Times.Once);
        result.Should().BeAssignableTo<IStatusCodeActionResult>().Which.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task GetLocation_WithGivenParameter_CallsRepoFunctionOnce()
    {
        //Arrange: prepare data
            var locationsController =
            new LocationsController(_locationRepositoryStub.Object, _mapper);

        //Act: call the method
        await locationsController.GetLocation(100);

        //Assert: compare expected result with actual
        _locationRepositoryStub.Verify(repo => repo.GetLocationById(100), Times.Once);
    }

    [Fact]
    public async Task GetLocation_WithExistingLocation_ReturnsOk()
    {
        //Arrange: prepare data
        _locationRepositoryStub
            .Setup(repo => repo.GetLocationById(It.IsAny<int>()))
            .ReturnsAsync(new Location());

        var locationsController =
            new LocationsController(_locationRepositoryStub.Object, _mapper);

        //Act: call the method
        var result = await locationsController.GetLocation(1);

        //Assert: compare expected result with actual
        result.Should().BeAssignableTo<IStatusCodeActionResult>().Which.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task GetLocation_WithNonExistingLocation_ReturnsNotFound()
    {
        //Arrange: prepare data
        _locationRepositoryStub
            .Setup(repo => repo.GetLocationById(It.IsAny<int>()))
            .ReturnsAsync((Location) null!);

        var locationsController =
            new LocationsController(_locationRepositoryStub.Object, _mapper);

        //Act: call the method
        var result = await locationsController.GetLocation(1000);

        //Assert: compare expected result with actual
        result.Should().BeAssignableTo<IStatusCodeActionResult>().Which.StatusCode.Should().Be(404);
    }

    [Fact]
    public async Task GetLocationMonths_WithGivenParameter_CallsRepoFunctionOnceAndReturnsOkResponse()
    {
        //Arrange: prepare data
        var locationsController =
            new LocationsController(_locationRepositoryStub.Object, _mapper);

        //Act: call the method
        var result = await locationsController.GetLocationMonths(1);
        
        //Assert: compare expected result with actual
        _locationRepositoryStub.Verify(repo => repo.GetLocationMonths(1), Times.Once);
        result.Should().BeAssignableTo<IStatusCodeActionResult>().Which.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task UpdateLocation_WithGivenParameter_CallsRepoFunctionOnce()
    {
        //Arrange: prepare data
        var locationsController =
            new LocationsController(_locationRepositoryStub.Object, _mapper);
        
        //Act: call the method
        await locationsController.UpdateLocation(1, new LocationCreateDto());

        //Assert: compare expected result with actual
        _locationRepositoryStub.Verify(repo => repo.UpdateLocation(1, It.IsAny<Location>()), Times.Once);
    }

    [Fact]
    public async Task UpdateLocation_WithUpdatedLocation_ReturnsOk()
    {
        //Arrange: prepare data
        _locationRepositoryStub
            .Setup(repo => repo.UpdateLocation(It.IsAny<int>(), It.IsAny<Location>()))
            .ReturnsAsync(new Location());

        var locationsController =
            new LocationsController(_locationRepositoryStub.Object, _mapper);

        //Act: call the method
        var result = await locationsController.UpdateLocation(100, new LocationCreateDto());

        //Assert: compare expected result with actual
        result.Should().BeAssignableTo<IStatusCodeActionResult>().Which.StatusCode.Should().Be(200);
    }

    //TODO: Not functioning correctly, setup invalid request
    [Fact]
    public async Task UpdateLocation_WithInvalidRequest_NeverCallsRepoFunctionAndReturnsBadRequest()
    {
        //Arrange: prepare data
        var locationsController =
            new LocationsController(_locationRepositoryStub.Object, _mapper);

        locationsController.ModelState.AddModelError("", "");

        //Act: call the method
        var result = await locationsController.UpdateLocation(100, new LocationCreateDto());

        //Assert: compare expected result with actual
        _locationRepositoryStub.Verify(repo => repo.UpdateLocation(100, new Location()), Times.Never);
        result.Should().BeAssignableTo<IStatusCodeActionResult>().Which.StatusCode.Should().Be(400);
    }

    [Fact]
    public async Task UpdateLocation_WithNonExistingLocation_ReturnsNotFound()
    {
        //Arrange: prepare data
        _locationRepositoryStub
            .Setup(repo => repo.UpdateLocation(It.IsAny<int>(), It.IsAny<Location>()))
            .ReturnsAsync((Location) null!);

        var locationsController =
            new LocationsController(_locationRepositoryStub.Object, _mapper);

        //Act: call the method
        var result = await locationsController.UpdateLocation(100, new LocationCreateDto());

        //Assert: compare expected result with actual
        result.Should().BeAssignableTo<IStatusCodeActionResult>().Which.StatusCode.Should().Be(404);
    }

    //TODO: Not functioning, figure out testing when parameters differ between controller and repo function (mapping)
    [Fact]
    public async Task CreateLocation_WithGivenParameter_CallsRepoFunctionOnce()
    {
        //Arrange: prepare data
        var locationsController =
            new LocationsController(_locationRepositoryStub.Object, _mapper);

        //Act: call the method
        await locationsController.CreateLocation(new LocationCreateDto());

        //Assert: compare expected result with actual
        _locationRepositoryStub.Verify(repo => repo.CreateLocation(It.IsAny<Location>()),
            Times.Once);
    }

    [Fact]
    public async Task CreateLocation_WithValidRequest_ReturnsOk()
    {
        //Arrange: prepare data
        var locationsController =
            new LocationsController(_locationRepositoryStub.Object, _mapper);

        //Act: call the method
        var result = await locationsController.CreateLocation(new LocationCreateDto());

        //Assert: compare expected result with actual
        result.Should().BeAssignableTo<IStatusCodeActionResult>().Which.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task CreateLocation_WithInvalidRequest_NeverCallsRepoFunctionAndReturnsBadRequest()
    {
        //Arrange: prepare data
        var locationsController =
            new LocationsController(_locationRepositoryStub.Object, _mapper);

        locationsController.ModelState.AddModelError("", "");

        //Act: call the method
        var result = await locationsController.CreateLocation(new LocationCreateDto());

        //Assert: compare expected result with actual
        _locationRepositoryStub.Verify(repo => repo.CreateLocation(It.IsAny<Location>()),
            Times.Never);
        result.Should().BeAssignableTo<IStatusCodeActionResult>().Which.StatusCode.Should().Be(400);
    }

    [Fact]
    public async Task CreateLocation_WithExistingLocation_NeverCallsRepoFunctionAndReturnsError()
    {
        //Arrange: prepare data
        _locationRepositoryStub
            .Setup(repo => repo.GetLocationByName(It.IsAny<string>()))
            .ReturnsAsync(new Location());

        var locationsController =
            new LocationsController(_locationRepositoryStub.Object, _mapper);

        //Act: call the method
        var result = await locationsController.CreateLocation(new LocationCreateDto());
        var errorMessage = locationsController.ModelState[""]!.Errors.First().ErrorMessage;

        //Assert: compare expected result with actual
        _locationRepositoryStub.Verify(repo => repo.CreateLocation(It.IsAny<Location>()),
            Times.Never);
        errorMessage.Should().NotBeNull().And.BeEquivalentTo("Location already exists");
        result.Should().BeAssignableTo<IStatusCodeActionResult>().Which.StatusCode.Should().Be(422);
    }

    [Fact]
    public async Task DeleteLocation_WithGivenParameter_CallsRepoFunctionOnce()
    {
        //Arrange: prepare data
        var locationsController =
            new LocationsController(_locationRepositoryStub.Object, _mapper);

        //Act: call the method
        await locationsController.DeleteLocation(1);

        //Assert: compare expected result with actual
        _locationRepositoryStub.Verify(repo => repo.DeleteLocation(1), Times.Once);
    }

    [Fact]
    public async Task DeleteLocation_WithExistingLocation_ReturnsOk()
    {
        //Arrange: prepare data
        _locationRepositoryStub
            .Setup(repo => repo.DeleteLocation(It.IsAny<int>()))
            .ReturnsAsync(new Location());

        var locationsController =
            new LocationsController(_locationRepositoryStub.Object, _mapper);

        //Act: call the method
        var result = await locationsController.DeleteLocation(1);

        //Assert: compare expected result with actual
        result.Should().BeAssignableTo<IStatusCodeActionResult>().Which.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task DeleteLocation_WithNonExistingLocation_ReturnsNotFound()
    {
        //Arrange: prepare data
        _locationRepositoryStub
            .Setup(repo => repo.DeleteLocation(It.IsAny<int>()))
            .ReturnsAsync((Location) null!);

        var locationsController =
            new LocationsController(_locationRepositoryStub.Object, _mapper);

        //Act: call the method
        var result = await locationsController.DeleteLocation(100);

        //Assert: compare expected result with actual
        result.Should().BeAssignableTo<IStatusCodeActionResult>().Which.StatusCode.Should().Be(404);
    }
}