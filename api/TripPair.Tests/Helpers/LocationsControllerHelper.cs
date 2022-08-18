using System;
using System.Collections.Generic;
using TripPair.Api.Models;

namespace TripPair.Tests.Helpers;

public static class LocationsControllerHelper
{
    public static IEnumerable<Location> ListOfLocations()
    {
        var location1 = CreateLocation(1, "location1", "month1");
        var location2 = CreateLocation(new Random().Next(), "location1", "month1");
        var location3 = CreateLocation(new Random().Next(), "location2", "month2");

        return new List<Location> {location1, location2, location3};
    }
    
    public static IEnumerable<LocationMonth> ListOfLocationMonths()
    {
        var locationMonth1 = CreateLocationMonth(1,  1);
        var locationMonth2 = CreateLocationMonth(new Random().Next(), 2);

        return new List<LocationMonth> {locationMonth1, locationMonth2};
    }

    public static Location CreateLocation(int id, string locationName, string month)
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

    private static LocationMonth CreateLocationMonth(int locationId, int monthId)
    {
        return new LocationMonth
        {
            Id = new Random().Next(),
            LocationId = locationId,
            Location = new Location(),
            MonthId = monthId,
            Month = new Month
            {
                Id = monthId,
                MonthLocations = new List<LocationMonth>(),
                Name = Guid.NewGuid().ToString()
            }
        };
    }
}