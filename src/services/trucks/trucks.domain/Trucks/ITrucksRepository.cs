using FluentResults;
using trucks.domain.SeedWork;
using Trucks.domain.Trucks.DbSnapshots;

namespace Trucks.domain.Trucks
{
    public interface ITrucksRepository : IRepository<Truck>
    {
        public void Add(TruckDbSnapshot dbSnapshot);
        public Task<Result<TruckDbSnapshot>> GetAsync(Guid truckId);
    }
}
