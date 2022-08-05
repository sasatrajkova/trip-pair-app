namespace tripPairAPI.Models;

public class Location
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string GoodMonthsDescription { get; set; }
    public IEnumerable<LocationMonth> LocationMonths { get; set; }
}