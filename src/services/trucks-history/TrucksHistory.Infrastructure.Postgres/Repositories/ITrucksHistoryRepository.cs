using TrucksHistory.Infrastructure.Postgres.DbModels;

namespace TrucksHistory.Infrastructure.Postgres.Repositories;
public interface ITrucksHistoryRepository
{
    Task<IEnumerable<TruckDbModel>> GetAllAsync();
}
