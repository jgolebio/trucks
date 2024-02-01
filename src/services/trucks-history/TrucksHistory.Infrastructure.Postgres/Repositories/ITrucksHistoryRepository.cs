using TrucksHistory.Infrastructure.Postgres.DbModels;

namespace TrucksHistory.Infrastructure.Postgres.Repositories;
public interface ITrucksHistoryRepository
{
    void Add(TruckHistoryDbModel truckHistoryDbModel);
    Task<IEnumerable<TruckHistoryDbModel>> GetAllAsync();
}
