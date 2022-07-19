namespace tripPairAPI.Data;

public class LocationDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string GoodMonthsDescription { get; set; }
    public IEnumerable<string> Months { get; set; }
}