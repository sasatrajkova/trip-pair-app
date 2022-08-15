using System;
using System.Collections.Generic;
using TripPair.Api.Data;
using TripPair.Api.Models;

namespace TripPair.Tests.Helpers;

public static class ResortsControllerHelper
{
    public static IEnumerable<Resort> ListOfCustomResorts()
    {
        var resort1 = CreateCustomResort("resort1", "climate1", "location1", "month1");
        var resort2 = CreateCustomResort("resort2", "climate1", "location1", "month1");
        var resort3 = CreateCustomResort("resort3", "climate2", "location1", "month2");
        var resort4 = CreateCustomResort(1, "resort4", "climate2", "location1", "month2");


        return new List<Resort> {resort1, resort2, resort3, resort4};
    }

    private static IEnumerable<Resort> ListOfRandomResorts()
    {
        return new List<Resort> {CreateRandomResort(), CreateRandomResort(), CreateRandomResort()};
    }
    
    private static Resort CreateCustomResort(string resortName, string climate, string locationName, string month)
    {
        return new Resort
        { 
            Id = new Random().Next(),
            Name = resortName,
            Climate = climate,
            Image = Guid.NewGuid().ToString(),
            Location = CreateCustomLocation(locationName, month),
            LocationId = new Random().Next()
        };
    }
    
    private static Resort CreateCustomResort(int id, string resortName, string climate, string locationName, string month)
    {
        return new Resort
        { 
            Id = id,
            Name = resortName,
            Climate = climate,
            Image = Guid.NewGuid().ToString(),
            Location = CreateCustomLocation(locationName, month),
            LocationId = new Random().Next()
        };
    }

    public static ResortDto CreateCustomResortDto(string resortName, string climate)
    {
        return new ResortDto
        { 
            Name = resortName,
            Climate = climate,
            Image = Guid.NewGuid().ToString(),
            LocationId = new Random().Next(),
            Location = new LocationDto()
        };
    }

    private static Location CreateCustomLocation(string locationName, string month)
    {
        return new Location
        {
            Name = locationName,
            LocationMonths = new List<LocationMonth>
            {
                new()
                {
                    Month = new Month()
                    {
                        Name = month
                    }
                }
            }
        };
    }
    
    private static Resort CreateRandomResort()
    {
        return new Resort
        {
            Name = Guid.NewGuid().ToString(),
            Climate = Guid.NewGuid().ToString(),
            Image = Guid.NewGuid().ToString(),
            Location = new Location(),
            LocationId = new Random().Next()
        };
    }
}