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

public class ResortRepositoryTests
{
    private static readonly MapperConfiguration MockMapper = new(config => config.AddProfile(new MappingProfiles()));
    private readonly IMapper _mapper = MockMapper.CreateMapper();
    private readonly Mock<IResortRepository> _resortRepositoryStub = new();
    
    [Fact]
    public async Task GetAllResorts_Always_ReturnsAListOfAllResorts()
    {
        //Arrange: prepare data

        //Act: call the method

        //Assert: compare expected result with actual

    }
    
    [Fact]
    public async Task GetResortsBySearch_Always_ReturnsAListOfFoundResorts()
    {
        //Arrange: prepare data

        //Act: call the method

        //Assert: compare expected result with actual

    }
    
    [Fact]
    public async Task GetResortById_WithExistingResort_ReturnsExpectedResort()
    {
        //Arrange: prepare data

        //Act: call the method

        //Assert: compare expected result with actual

    }

    [Fact]
    public async Task GetResortById_WithNonExistingResort_ReturnsNull()
    {
        //Arrange: prepare data

        //Act: call the method

        //Assert: compare expected result with actual

    }
    
    [Fact]
    public async Task GetResortByName_WithExistingResort_ReturnsExpectedResort()
    {
        //Arrange: prepare data

        //Act: call the method

        //Assert: compare expected result with actual

    }
    
    [Fact]
    public async Task GetResortByName_WithNonExistingResort_ReturnsNull()
    {
        //Arrange: prepare data

        //Act: call the method

        //Assert: compare expected result with actual

    }

    [Fact]
    public async Task CreateResort_Always_AddsGivenResortToDb()
    {
        //Arrange: prepare data

        //Act: call the method

        //Assert: compare expected result with actual

    }
    
    [Fact]
    public async Task CreateResort_Always_ReturnsCreatedResort()
    {
        //Arrange: prepare data

        //Act: call the method

        //Assert: compare expected result with actual

    }
    
    [Fact]
    public async Task DeleteResort_WithExistingResort_RemovesGivenResortFromDb()
    {
        //Arrange: prepare data

        //Act: call the method

        //Assert: compare expected result with actual

    }
    
    [Fact]
    public async Task DeleteResort_WithExistingResort_ReturnsDeletedResort()
    {
        //Arrange: prepare data

        //Act: call the method

        //Assert: compare expected result with actual

    }

    [Fact]
    public async Task DeleteResort_WithNonExistingResort_ReturnsNull()
    {
        //Arrange: prepare data

        //Act: call the method

        //Assert: compare expected result with actual

    }
}