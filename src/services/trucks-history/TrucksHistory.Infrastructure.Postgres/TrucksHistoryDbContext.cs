using Microsoft.EntityFrameworkCore;
using TrucksHistory.Infrastructure.Postgres.DbModels;
using TrucksHistory.Infrastructure.Postgres.EntityConfigurations;

namespace TrucksHistory.Infrastructure.Postgres;
public class TrucksHistoryDbContext : DbContext
{
    public DbSet<TruckDbModel> Trucks { get; set; }

    public TrucksHistoryDbContext(DbContextOptions<TrucksHistoryDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TrucksConfiguration());
    }
}
