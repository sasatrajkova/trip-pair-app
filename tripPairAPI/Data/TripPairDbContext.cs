using Microsoft.EntityFrameworkCore;
using tripPairAPI.Models;

namespace tripPairAPI.Data;

public class TripPairDbContext : DbContext
{
    public TripPairDbContext(DbContextOptions<TripPairDbContext> options)
        : base(options) {}
    public DbSet<Resort> Resorts { get; set; }
    
}