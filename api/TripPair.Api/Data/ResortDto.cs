namespace TripPair.Api.Data;

public class ResortDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Climate { get; set; }
    public string Image { get; set; }
    public LocationDto Location { get; set; }
}