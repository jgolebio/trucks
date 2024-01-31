using Microsoft.EntityFrameworkCore;
using TrucksHistory.Infrastructure.Postgres.DbModels;

namespace TrucksHistory.Infrastructure.Postgres.Repositories;
public class TrucksHistoryRepository : ITrucksHistoryRepository
{
    private readonly TrucksHistoryDbContext _dbContext;

    public TrucksHistoryRepository(TrucksHistoryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<TruckDbModel>> GetAllAsync() =>
        await _dbContext.Trucks.ToListAsync();
}
