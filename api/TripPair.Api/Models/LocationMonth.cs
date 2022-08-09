namespace TripPair.Api.Models;

public class LocationMonth
{
    public int Id { get; set; }
    public int LocationId { get; set; }
    public Location Location { get; set; }
    public int MonthId { get; set; }
    public Month Month { get; set; }
}