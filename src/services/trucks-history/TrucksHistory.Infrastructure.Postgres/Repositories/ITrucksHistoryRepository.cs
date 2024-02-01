using TrucksHistory.Infrastructure.Postgres.DbModels;

namespace TrucksHistory.Infrastructure.Postgres.Repositories;
public interface ITrucksHistoryRepository
{
    void Add(TruckHistoryDbModel truckHistoryDbModel);
    Task<TruckHistoryDbModel?> FindByIdAsync(Guid id);
    Task<IEnumerable<TruckHistoryDbModel>> GetAllAsync();
    void UpdateDbModel(TruckHistoryDbModel dbModel);
}
