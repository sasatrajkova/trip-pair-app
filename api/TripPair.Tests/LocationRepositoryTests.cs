using System.Threading.Tasks;
using AutoMapper;
using Moq;
using TripPair.Api.Helpers;
using TripPair.Api.Interfaces;
using Xunit;

namespace TripPair.Tests;

//TODO: Location repo tests
public class LocationRepositoryTests
{
    private static readonly MapperConfiguration MockMapper = new(config => config.AddProfile(new MappingProfiles()));
    private readonly IMapper _mapper = MockMapper.CreateMapper();
    private readonly Mock<IResortRepository> _resortRepositoryStub = new();

    [Fact]
    public async Task GetAllLocations_Always_ReturnsAListOfAllLocations()
    {
        //Arrange: prepare data

        //Act: call the method

        //Assert: compare expected result with actual
    }

    [Fact]
    public async Task GetLocationById_WithExistingLocation_ReturnsExpectedLocation()
    {
        //Arrange: prepare data

        //Act: call the method

        //Assert: compare expected result with actual
    }

    [Fact]
    public async Task GetLocationById_WithNonExistingLocation_ReturnsNull()
    {
        //Arrange: prepare data

        //Act: call the method

        //Assert: compare expected result with actual
    }

    [Fact]
    public async Task GetLocationByName_WithExistingLocation_ReturnsExpectedLocation()
    {
        //Arrange: prepare data

        //Act: call the method

        //Assert: compare expected result with actual
    }

    [Fact]
    public async Task GetLocationByName_WithNonExistingLocation_ReturnsNull()
    {
        //Arrange: prepare data

        //Act: call the method

        //Assert: compare expected result with actual
    }

    [Fact]
    public async Task GetLocationMonths_Always_ReturnsAListOfAllLocationMonths()
    {
        //Arrange: prepare data

        //Act: call the method

        //Assert: compare expected result with actual
    }

    [Fact]
    public async Task UpdateLocation_WithExistingLocation_ReturnsUpdatedLocation()
    {
        //Arrange: prepare data

        //Act: call the method

        //Assert: compare expected result with actual
    }

    [Fact]
    public async Task UpdateLocation_WithExistingLocation_UpdatesGivenLocationMonthsInDb()
    {
        //Arrange: prepare data

        //Act: call the method

        //Assert: compare expected result with actual
    }

    [Fact]
    public async Task UpdateLocation_WithExistingLocation_UpdatesGivenLocationInDb()
    {
        //Arrange: prepare data

        //Act: call the method

        //Assert: compare expected result with actual
    }

    [Fact]
    public async Task UpdateLocation_WithNonExistingLocation_ReturnsNull()
    {
        //Arrange: prepare data

        //Act: call the method

        //Assert: compare expected result with actual
    }

    [Fact]
    public async Task CreateLocation_Always_AddsGivenLocationToDb()
    {
        //Arrange: prepare data

        //Act: call the method

        //Assert: compare expected result with actual
    }

    [Fact]
    public async Task CreateLocation_Always_ReturnsCreatedLocation()
    {
        //Arrange: prepare data

        //Act: call the method

        //Assert: compare expected result with actual
    }

    [Fact]
    public async Task DeleteLocation_WithExistingLocation_RemovesGivenLocationFromDb()
    {
        //Arrange: prepare data

        //Act: call the method

        //Assert: compare expected result with actual
    }

    [Fact]
    public async Task DeleteLocation_WithExistingLocation_ReturnsDeletedLocation()
    {
        //Arrange: prepare data

        //Act: call the method

        //Assert: compare expected result with actual
    }

    [Fact]
    public async Task DeleteLocation_WithExistingLocation_RemovesLocationMonthsOfAGivenLocationFromDb()
    {
        //Arrange: prepare data

        //Act: call the method

        //Assert: compare expected result with actual
    }

    [Fact]
    public async Task DeleteLocation_WithNonExistingLocation_ReturnsNull()
    {
        //Arrange: prepare data

        //Act: call the method

        //Assert: compare expected result with actual
    }
}