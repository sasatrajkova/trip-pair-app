namespace tripPairAPI.Data;

public class LocationCreateDto
{
    public string Name { get; set; }
    public string GoodMonthsDescription { get; set; }
    public IEnumerable<LocationMonthCreateDto> LocationMonths { get; set; }
}