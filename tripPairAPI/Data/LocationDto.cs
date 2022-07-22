using tripPairAPI.Models;

namespace tripPairAPI.Data;

public class LocationDto
{
    public string Name { get; set; }
    public string GoodMonthsDescription { get; set; }
    public IEnumerable<LocationMonth> LocationMonths { get; set; }

}