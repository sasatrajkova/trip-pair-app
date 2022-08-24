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
        var statusResult = result.StatusCode;

        //Assert: compare expected result with actual
        _resortRepositoryStub.Verify(repo => repo.GetAllResorts(), Times.Once);
        statusResult.Should().Be(200);
    }

    [Fact]
    public async Task GetResortsBySearch_WithGivenParameter_CallsRepoFunctionOnceAndReturnsOk()
    {
        //Arrange: prepare data
        var resortsController =
            new ResortsController(_resortRepositoryStub.Object, _mapper);

        //Act: call the method
        var result = await resortsController.GetResortsBySearch("resort1");
        var statusResult = result.StatusCode;

        //Assert: compare expected result with actual
        _resortRepositoryStub.Verify(repo => repo.GetResortsBySearch("resort1"), Times.Once);
        statusResult.Should().Be(200);
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
        var statusCodeResult = (IStatusCodeActionResult) result;
        var statusCode = statusCodeResult.StatusCode;

        //Assert: compare expected result with actual
        statusCode.Should().Be(200);
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
        var statusCodeResult = (IStatusCodeActionResult) result;
        var statusCode = statusCodeResult.StatusCode;

        //Assert: compare expected result with actual
        statusCode.Should().Be(404);
    }


    //TODO: Not functioning, figure out testing when parameters between controller and repo function (mapping)
    [Fact]
    public async Task CreateResort_WithGivenParameter_CallsRepoFunctionOnce()
    {
        //Arrange: prepare data
        var resortCreateDto = new ResortCreateDto
        {
            Climate = "Test",
            Image = "Test",
            LocationId = 1,
            Name = "Test"
        };

        _resortRepositoryStub
            .Setup(repo => repo.CreateResort(It.IsAny<Resort>()))
            .ReturnsAsync((Resort r) => r);

        var resortsController =
            new ResortsController(_resortRepositoryStub.Object, _mapper);

        //Act: call the method
        await resortsController.CreateResort(resortCreateDto);

        //Assert: compare expected result with actual
        _resortRepositoryStub.Verify(repo => repo.CreateResort(_mapper.Map<Resort>(resortCreateDto)), Times.Once);
    }

    [Fact]
    public async Task CreateResort_WithValidRequest_ReturnsOk()
    {
        //Arrange: prepare data
        var resortsController =
            new ResortsController(_resortRepositoryStub.Object, _mapper);

        //Act: call the method
        var result = await resortsController.CreateResort(new ResortCreateDto());
        var statusCodeResult = (IStatusCodeActionResult) result;
        var statusCode = statusCodeResult.StatusCode;

        //Assert: compare expected result with actual
        statusCode.Should().Be(200);
    }

    //TODO: Not functioning correctly, setup invalid request
    [Fact]
    public async Task CreateResort_WithInvalidRequest_NeverCallsRepoFunctionAndReturnsBadRequest()
    {
        //Arrange: prepare data
        var resortCreateDto = new ResortCreateDto
        {
            Climate = "Test",
            Image = "Test",
            LocationId = 1,
            Name = "Test"
        };

        var resortsController =
            new ResortsController(_resortRepositoryStub.Object, _mapper);

        resortsController.ModelState.AddModelError("", "");

        //Act: call the method
        var result = await resortsController.CreateResort(resortCreateDto);
        var statusCodeResult = (IStatusCodeActionResult) result;
        var statusCode = statusCodeResult.StatusCode;

        //Assert: compare expected result with actual
        _resortRepositoryStub.Verify(repo => repo.CreateResort(_mapper.Map<Resort>(resortCreateDto)), Times.Never);
        statusCode.Should().Be(400);
    }

    //TODO Not functioning correctly, figure out how to setup repo for null return for existing resort
    [Fact]
    public async Task CreateResort_WithExistingResort_NeverCallsRepoFunctionAndReturnsError()
    {
        //Arrange: prepare data
        var resortCreateDto = new ResortCreateDto
        {
            Climate = "Test",
            Image = "Test",
            LocationId = 1,
            Name = "Test"
        };

        var resortsController =
            new ResortsController(_resortRepositoryStub.Object, _mapper);

        resortsController.ModelState.AddModelError("", "Resort already exists");

        //Act: call the method
        var result = await resortsController.CreateResort(resortCreateDto);
        var statusCodeResult = (IStatusCodeActionResult) result;
        var statusCode = statusCodeResult.StatusCode;

        //Assert: compare expected result with actual
        _resortRepositoryStub.Verify(repo => repo.CreateResort(_mapper.Map<Resort>(resortCreateDto)), Times.Never);
        statusCode.Should().Be(422);
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
        var statusCodeResult = (IStatusCodeActionResult) result;
        var statusCode = statusCodeResult.StatusCode;

        //Assert: compare expected result with actual
        statusCode.Should().Be(200);
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
        var statusCodeResult = (IStatusCodeActionResult) result;
        var statusCode = statusCodeResult.StatusCode;

        //Assert: compare expected result with actual
        statusCode.Should().Be(404);
    }
}