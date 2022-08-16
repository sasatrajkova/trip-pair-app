namespace TripPair.Api.Data;

public class LocationDto
{
    public string Name { get; set; }
    public string GoodMonthsDescription { get; set; }
    public IEnumerable<LocationMonthDto> LocationMonths { get; set; }
}