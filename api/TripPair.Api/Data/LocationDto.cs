namespace TripPair.Api.Data;

public class LocationDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string GoodMonthsDescription { get; set; }
    public IEnumerable<LocationMonthDto> LocationMonths { get; set; }
}