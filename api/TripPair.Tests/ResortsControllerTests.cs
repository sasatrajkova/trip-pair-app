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
using static TripPair.Tests.Helpers.ResortsControllerHelper;

namespace TripPair.Tests;

public class ResortsControllerTests
{
    private static readonly MapperConfiguration MockMapper = new(config => config.AddProfile(new MappingProfiles()));
    private readonly IMapper _mapper = MockMapper.CreateMapper();
    private readonly Mock<IResortRepository> _resortRepositoryStub = new();

    //Naming convention: UnitOfWork_StateUnderTest_ExpectedBehavior()

    [Fact]
    public async Task GetAllResorts_Always_ReturnsOkResponseWithAllResortDtos()
    {
        //Arrange: prepare data
        var availableResorts = ListOfResorts().ToList();

        _resortRepositoryStub
            .Setup(repo => repo.GetAllResorts())
            .ReturnsAsync(availableResorts);

        var resortsController =
            new ResortsController(_resortRepositoryStub.Object, _mapper);

        var expectedResortDtosCount = availableResorts.Count;

        //Act: call the method
        var result = await resortsController.GetAllResorts();
        var obj = result.Value as IEnumerable<ResortDto>;

        //Assert: compare expected result with actual
        obj?.Count().Should().Be(expectedResortDtosCount);
    }

    //TODO: Clarify if this is necessary - this is testing more the repo not controller
    [Fact]
    public async Task GetResortsBySearch_WithMatchingResortNames_ReturnsOkResponseWithExpectedResortDtos()
    {
        //Arrange: prepare data
        var availableResorts = ListOfResorts();

        _resortRepositoryStub
            .Setup(repo => repo.GetResortsBySearch(It.IsAny<string>()))
            .ReturnsAsync((string s) => availableResorts.Where(r => r.Name == s));

        var resortsController =
            new ResortsController(_resortRepositoryStub.Object, _mapper);

        var expectedResortDtos = _mapper.Map<List<ResortDto>>(availableResorts.Where(r => r.Name == "resort1"));

        //Act: call the method
        var result = await resortsController.GetResortsBySearch("resort1");
        var obj = result.Value as List<ResortDto>;

        //Assert: compare expected result with actual
        obj?.Should().BeEquivalentTo(expectedResortDtos);
        result.Should().BeOfType<OkObjectResult>();
    }

    //TODO: Clarify if this is necessary - this is testing more the repo not controller
    [Fact]
    public async Task GetResortsBySearch_WithMatchingResortLocations_ReturnsOkResponseWithExpectedResortDtos()
    {
        //Arrange: prepare data
        var availableResorts = ListOfResorts();

        _resortRepositoryStub
            .Setup(repo => repo.GetResortsBySearch(It.IsAny<string>()))
            .ReturnsAsync((string s) => availableResorts.Where(r => r.Location.Name == s));

        var resortsController =
            new ResortsController(_resortRepositoryStub.Object, _mapper);

        var expectedResortDtos =
            _mapper.Map<List<ResortDto>>(availableResorts.Where(r => r.Location.Name == "location1"));

        //Act: call the method
        var result = await resortsController.GetResortsBySearch("location1");
        var obj = result.Value as List<ResortDto>;

        //Assert: compare expected result with actual
        obj?.Should().BeEquivalentTo(expectedResortDtos);
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task GetResortsBySearch_WithMatchingResortClimates_ReturnsOkResponseWithExpectedResortDtos()
    {
        //Arrange: prepare data
        var availableResorts = ListOfResorts();

        _resortRepositoryStub
            .Setup(repo => repo.GetResortsBySearch(It.IsAny<string>()))
            .ReturnsAsync((string s) => availableResorts.Where(r => r.Climate == s));

        var resortsController =
            new ResortsController(_resortRepositoryStub.Object, _mapper);

        var expectedResortDtos = _mapper.Map<List<ResortDto>>(availableResorts.Where(r => r.Climate == "climate1"));

        //Act: call the method
        var result = await resortsController.GetResortsBySearch("climate1");
        var obj = result.Value as List<ResortDto>;

        //Assert: compare expected result with actual
        obj?.Should().BeEquivalentTo(expectedResortDtos);
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task GetResort_WithExistingResort_ReturnsOkResponseWithExpectedResortDto()
    {
        //Arrange: prepare data
        var availableResorts = ListOfResorts();

        _resortRepositoryStub
            .Setup(repo => repo.GetResortById(It.IsAny<int>()))
            .ReturnsAsync((int i) => availableResorts.FirstOrDefault(r => r.Id == i));

        var resortsController =
            new ResortsController(_resortRepositoryStub.Object, _mapper);

        var expectedResortDto = _mapper.Map<ResortDto>(availableResorts.FirstOrDefault(r => r.Id == 1));

        //Act: call the method
        var result = await resortsController.GetResort(1) as OkObjectResult;
        var obj = result?.Value as ResortDto;

        //Assert: compare expected result with actual
        obj.Should().BeEquivalentTo(expectedResortDto);
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task GetResort_WithNonExistingResort_ReturnsNotFound()
    {
        //Arrange: prepare data
        var availableResorts = ListOfResorts();

        _resortRepositoryStub
            .Setup(repo => repo.GetResortById(It.IsAny<int>()))
            .ReturnsAsync((int i) => availableResorts.FirstOrDefault(r => r.Id == i));

        var resortsController =
            new ResortsController(_resortRepositoryStub.Object, _mapper);

        //Act: call the method
        var result = await resortsController.GetResort(100);

        //Assert: compare expected result with actual
        result.Should().BeOfType<NotFoundResult>();
    }

    //TODO Check if this is a productive test
    [Fact]
    public async Task CreateResort_WithCreatedResort_ReturnsOkResponseWithCreatedResortDto()
    {
        //Arrange: prepare data
        var existingLocation = LocationsControllerHelper.CreateLocation(1, "location5", "month1");

        var resortToCreate = new ResortCreateDto
        {
            Name = "resort5",
            Climate = "climate2",
            Image = "resort5.jpg",
            LocationId = existingLocation.Id
        };

        _resortRepositoryStub
            .Setup(repo => repo.CreateResort(It.IsAny<Resort>()))
            .ReturnsAsync((Resort r) => new Resort
            {
                Id = r.Id, Name = r.Name, Climate = r.Climate, Image = r.Image, Location = r.Location,
                LocationId = r.LocationId
            });

        var resortsController =
            new ResortsController(_resortRepositoryStub.Object, _mapper);

        var expectedResortDto = _mapper.Map<ResortDto>(_mapper.Map<Resort>(resortToCreate));

        //Act: call the method
        var result = await resortsController.CreateResort(resortToCreate) as OkObjectResult;
        var obj = result?.Value as ResortDto;

        //Assert: compare expected result with actual
        obj.Should().BeEquivalentTo(expectedResortDto);
        result.Should().BeOfType<OkObjectResult>();
    }

    //TODO Check if this is a productive test
    [Fact]
    public async Task CreateResort_WithInvalidRequest_ReturnsBadRequest()
    {
        //Arrange: prepare data
        _resortRepositoryStub
            .Setup(repo => repo.CreateResort(It.IsAny<Resort>()))
            .ReturnsAsync((Resort r) => new Resort
            {
                Id = r.Id, Name = r.Name, Climate = r.Climate, Image = r.Image, Location = r.Location,
                LocationId = r.LocationId
            });

        var resortsController =
            new ResortsController(_resortRepositoryStub.Object, _mapper);

        resortsController.ModelState.AddModelError("", "Model state invalid");

        //Act: call the method
        var result = await resortsController.CreateResort(new ResortCreateDto());

        //Assert: compare expected result with actual
        result.Should().BeOfType<BadRequestResult>();
    }

    //TODO Check if this is a productive test
    [Fact]
    public async Task CreateResort_WithExistingResort_ReturnsResortAlreadyExists()
    {
        //Arrange: prepare data
        _resortRepositoryStub
            .Setup(repo => repo.CreateResort(It.IsAny<Resort>()))
            .ReturnsAsync((Resort r) => new Resort
            {
                Id = r.Id, Name = r.Name, Climate = r.Climate, Image = r.Image, Location = r.Location,
                LocationId = r.LocationId
            });

        var resortsController =
            new ResortsController(_resortRepositoryStub.Object, _mapper);

        resortsController.ModelState.AddModelError("", "Resort already exists");

        //Act: call the method
        var result = await resortsController.CreateResort(new ResortCreateDto());

        //Assert: compare expected result with actual
        result.Should().BeOfType<BadRequestResult>();
    }

    [Fact]
    public async Task DeleteResort_WithExistingResort_ReturnsOkResponseWithDeletedResortDto()
    {
        //Arrange: prepare data
        var availableResorts = ListOfResorts();

        _resortRepositoryStub
            .Setup(repo => repo.DeleteResort(It.IsAny<int>()))
            .ReturnsAsync((int i) => availableResorts.FirstOrDefault(r => r.Id == i));

        var resortsController =
            new ResortsController(_resortRepositoryStub.Object, _mapper);

        var expectedResortDto = _mapper.Map<ResortDto>(availableResorts.FirstOrDefault(r => r.Id == 1));

        //Act: call the method
        var result = await resortsController.DeleteResort(1) as OkObjectResult;
        var obj = result?.Value as ResortDto;

        //Assert: compare expected result with actual
        obj.Should().BeEquivalentTo(expectedResortDto);
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task DeleteResort_WithNonExistingResort_ReturnsNotFound()
    {
        //Arrange: prepare data
        var availableResorts = ListOfResorts();

        _resortRepositoryStub
            .Setup(repo => repo.DeleteResort(It.IsAny<int>()))
            .ReturnsAsync((int i) => availableResorts.FirstOrDefault(r => r.Id == i));

        var resortsController =
            new ResortsController(_resortRepositoryStub.Object, _mapper);

        //Act: call the method
        var result = await resortsController.DeleteResort(100);

        //Assert: compare expected result with actual
        result.Should().BeOfType<NotFoundResult>();
    }
}