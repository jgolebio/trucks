using FluentResults;
using trucks.domain.SeedWork;

namespace Trucks.domain.Trucks
{
    public interface ITrucksRepository : IRepository<Truck>
    {
        public void Add(Truck model);
        void Delete(Truck model);
        public Task<Result<Truck>> GetAsync(Guid truckId, CancellationToken cancellationToken);
        Task<Result<bool>> IsAnyWithCodeAsync(string code);
        void Update(Truck model);
    }
}
