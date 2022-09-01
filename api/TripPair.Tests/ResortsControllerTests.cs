using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using TripPair.Api.Controllers;
using TripPair.Api.Data;
using TripPair.Api.Helpers;
using TripPair.Api.Interfaces;
using TripPair.Api.Models;
using Xunit;

namespace TripPair.Tests;

public class ResortsControllerTests
{
    private static readonly MapperConfiguration MockMapper = new(config => config.AddProfile(new MappingProfiles()));
    private readonly IMapper _mapper = MockMapper.CreateMapper();
    private readonly Mock<IResortRepository> _resortRepositoryStub = new();

    //Naming convention: UnitOfWork_StateUnderTest_ExpectedBehavior()

    [Fact]
    public async Task GetAllResorts_Always_CallsRepoFunctionOnceAndReturnsOk()
    {
        //Arrange: prepare data
        var resortsController =
            new ResortsController(_resortRepositoryStub.Object, _mapper);

        //Act: call the method
        var result = await resortsController.GetAllResorts();

        //Assert: compare expected result with actual
        _resortRepositoryStub.Verify(repo => repo.GetAllResorts(), Times.Once);
        result.Should().BeAssignableTo<IStatusCodeActionResult>().Which.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task GetResortsBySearch_WithGivenParameter_CallsRepoFunctionOnceAndReturnsOk()
    {
        //Arrange: prepare data
        var resortsController =
            new ResortsController(_resortRepositoryStub.Object, _mapper);

        //Act: call the method
        var result = await resortsController.GetResortsBySearch("resort1");

        //Assert: compare expected result with actual
        _resortRepositoryStub.Verify(repo => repo.GetResortsBySearch("resort1"), Times.Once);
        result.Should().BeAssignableTo<OkObjectResult>().Which.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task GetResort_WithGivenParameter_CallsRepoFunctionOnce()
    {
        //Arrange: prepare data
        var resortsController =
            new ResortsController(_resortRepositoryStub.Object, _mapper);

        //Act: call the method
        await resortsController.GetResort(100);

        //Assert: compare expected result with actual
        _resortRepositoryStub.Verify(repo => repo.GetResortById(100), Times.Once);
    }

    [Fact]
    public async Task GetResort_WithExistingResort_ReturnsOk()
    {
        //Arrange: prepare data
        _resortRepositoryStub
            .Setup(repo => repo.GetResortById(It.IsAny<int>()))
            .ReturnsAsync(new Resort());

        var resortsController =
            new ResortsController(_resortRepositoryStub.Object, _mapper);

        //Act: call the method
        var result = await resortsController.GetResort(1);

        //Assert: compare expected result with actual
        result.Should().BeAssignableTo<IStatusCodeActionResult>().Which.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task GetResort_WithNonExistingResort_ReturnsNotFound()
    {
        //Arrange: prepare data
        _resortRepositoryStub
            .Setup(repo => repo.GetResortById(It.IsAny<int>()))
            .ReturnsAsync((Resort) null!);

        var resortsController =
            new ResortsController(_resortRepositoryStub.Object, _mapper);

        //Act: call the method
        var result = await resortsController.GetResort(1000);

        //Assert: compare expected result with actual
        result.Should().BeAssignableTo<IStatusCodeActionResult>().Which.StatusCode.Should().Be(404);
    }

    [Fact]
    public async Task CreateResort_WithGivenParameter_CallsRepoFunctionOnce()
    {
        //Arrange: prepare data
        var resortsController =
            new ResortsController(_resortRepositoryStub.Object, _mapper);

        //Act: call the method
        await resortsController.CreateResort(new ResortCreateDto());

        //Assert: compare expected result with actual
        _resortRepositoryStub.Verify(repo => repo.CreateResort(It.IsAny<Resort>()), Times.Once);
    }

    [Fact]
    public async Task CreateResort_WithValidRequest_ReturnsOk()
    {
        //Arrange: prepare data
        var resortsController =
            new ResortsController(_resortRepositoryStub.Object, _mapper);

        //Act: call the method
        var result = await resortsController.CreateResort(new ResortCreateDto());

        //Assert: compare expected result with actual
        result.Should().BeAssignableTo<IStatusCodeActionResult>().Which.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task CreateResort_WithInvalidRequest_NeverCallsRepoFunctionAndReturnsBadRequest()
    {
        //Arrange: prepare data
        var resortsController =
            new ResortsController(_resortRepositoryStub.Object, _mapper);

        resortsController.ModelState.AddModelError("", "");

        //Act: call the method
        var result = await resortsController.CreateResort(new ResortCreateDto());

        //Assert: compare expected result with actual
        _resortRepositoryStub.Verify(repo => repo.CreateResort(It.IsAny<Resort>()), Times.Never);
        result.Should().BeAssignableTo<IStatusCodeActionResult>().Which.StatusCode.Should().Be(400);
    }

    [Fact]
    public async Task CreateResort_WithExistingResort_NeverCallsRepoFunctionAndReturnsError()
    {
        //Arrange: prepare data
        _resortRepositoryStub
            .Setup(repo => repo.GetResortByName(It.IsAny<string>(), It.IsAny<int>()))
            .ReturnsAsync(new Resort());

        var resortsController =
            new ResortsController(_resortRepositoryStub.Object, _mapper);

        //Act: call the method
        var result = await resortsController.CreateResort(new ResortCreateDto());
        var errorMessage = resortsController.ModelState[""]!.Errors.First().ErrorMessage;

        //Assert: compare expected result with actual
        _resortRepositoryStub.Verify(repo => repo.CreateResort(It.IsAny<Resort>()), Times.Never);
        errorMessage.Should().NotBeNull().And.BeEquivalentTo("Resort already exists");
        result.Should().BeAssignableTo<IStatusCodeActionResult>().Which.StatusCode.Should().Be(422);
    }

    [Fact]
    public async Task DeleteResort_WithGivenParameter_CallsRepoFunctionOnce()
    {
        //Arrange: prepare data
        var resortsController =
            new ResortsController(_resortRepositoryStub.Object, _mapper);

        //Act: call the method
        await resortsController.DeleteResort(1);

        //Assert: compare expected result with actual
        _resortRepositoryStub.Verify(repo => repo.DeleteResort(1), Times.Once);
    }

    [Fact]
    public async Task DeleteResort_WithExistingResort_ReturnsOk()
    {
        //Arrange: prepare data
        _resortRepositoryStub
            .Setup(repo => repo.DeleteResort(It.IsAny<int>()))
            .ReturnsAsync(new Resort());

        var resortsController =
            new ResortsController(_resortRepositoryStub.Object, _mapper);

        //Act: call the method
        var result = await resortsController.DeleteResort(1);

        //Assert: compare expected result with actual
        result.Should().BeAssignableTo<IStatusCodeActionResult>().Which.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task DeleteResort_WithNonExistingResort_NotFound()
    {
        //Arrange: prepare data
        _resortRepositoryStub
            .Setup(repo => repo.DeleteResort(It.IsAny<int>()))
            .ReturnsAsync((Resort) null!);

        var resortsController =
            new ResortsController(_resortRepositoryStub.Object, _mapper);

        //Act: call the method
        var result = await resortsController.DeleteResort(100);

        //Assert: compare expected result with actual
        result.Should().BeAssignableTo<IStatusCodeActionResult>().Which.StatusCode.Should().Be(404);
    }
}