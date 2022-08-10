namespace TripPair.Api.Models;

public class Month
{
    public int Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<LocationMonth> MonthLocations { get; set; }
}