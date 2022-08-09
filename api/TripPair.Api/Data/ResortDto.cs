namespace TripPair.Api.Data;

public class ResortDto
{
    public string Name { get; set; }
    public string Climate { get; set; }
    public string Image { get; set; }
    public int LocationId { get; set; }
    public LocationDto Location { get; set; }
}