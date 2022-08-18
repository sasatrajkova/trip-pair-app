using System;
using System.Collections.Generic;
using TripPair.Api.Models;

namespace TripPair.Tests.Helpers;

public static class LocationsControllerHelper
{
    public static IEnumerable<Location> ListOfLocations()
    {
        var location1 = CreateLocation(new Random().Next(), "location1", "month1");
        var location2 = CreateLocation(new Random().Next(), "location1", "month1");
        var location3 = CreateLocation(new Random().Next(), "location2", "month2");

        return new List<Location> {location1, location2, location3};
    }

    private static Location CreateLocation(int id, string locationName, string month)
    {
        return new Location
        {
            Id = id,
            Name = locationName,
            LocationMonths = new List<LocationMonth>
            {
                new()
                {
                    Month = new Month
                    {
                        Name = month
                    }
                }
            }
        };
    }
}