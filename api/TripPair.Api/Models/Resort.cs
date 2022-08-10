namespace TripPair.Api.Models;

public class Resort
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Climate { get; set; }
    public string Image { get; set; }
    public int LocationId { get; set; }
    public Location Location { get; set; }
}