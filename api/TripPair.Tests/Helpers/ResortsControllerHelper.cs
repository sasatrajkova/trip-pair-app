using System;
using System.Collections.Generic;
using TripPair.Api.Models;
using static TripPair.Tests.Helpers.LocationsControllerHelper;


namespace TripPair.Tests.Helpers;

public static class ResortsControllerHelper
{
    public static IEnumerable<Resort> ListOfResorts()
    {
        var resort1 = CreateResort(new Random().Next(), "resort1", "climate1", "location1", "month1");
        var resort2 = CreateResort(new Random().Next(), "resort2", "climate1", "location1", "month1");
        var resort3 = CreateResort(new Random().Next(), "resort3", "climate2", "location2", "month2");
        var resort4 = CreateResort(1, "resort4", "climate2", "location2", "month2");
        var resort5 = CreateResort(new Random().Next(), "resort1", "climate2", "location3", "month3");
        var resort6 = CreateResort(new Random().Next(), "resort1", "climate2", "location4", "month4");

        return new List<Resort> {resort1, resort2, resort3, resort4, resort5, resort6};
    }

    private static Resort CreateResort(int id, string resortName, string climate, string locationName, string month)
    {
        return new Resort
        {
            Id = id,
            Name = resortName,
            Climate = climate,
            Image = Guid.NewGuid().ToString(),
            Location = CreateLocation(new Random().Next(), locationName, month),
            LocationId = new Random().Next()
        };
    }
}