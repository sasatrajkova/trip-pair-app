using Microsoft.EntityFrameworkCore;
using TripPair.Api.Models;

namespace TripPair.Api.Data;

public class TripPairDbContext : DbContext
{
    public TripPairDbContext(DbContextOptions<TripPairDbContext> options)
        : base(options)
    {
    }

    public DbSet<Resort> Resorts { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Month> Months { get; set; }
    public DbSet<LocationMonth> LocationMonths { get; set; }
}