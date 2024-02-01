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

    public void Add(TruckHistoryDbModel truckHistoryDbModel) =>
        _dbContext.Trucks.Add(truckHistoryDbModel);

    public async Task<IEnumerable<TruckHistoryDbModel>> GetAllAsync() =>
        await _dbContext.Trucks.ToListAsync();

    public async Task<TruckHistoryDbModel?> FindByIdAsync(Guid id) =>
        await _dbContext.Trucks.FirstOrDefaultAsync(x => x.Id == id);

    public void UpdateDbModel(TruckHistoryDbModel dbModel) =>
        _dbContext.Trucks.Update(dbModel);
}
