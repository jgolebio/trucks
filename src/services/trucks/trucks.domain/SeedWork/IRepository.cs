using Trucks.Domain.SeedWork;

namespace Trucks.Domain.SeedWork;

public interface IRepository<T> where T : IAggregateRoot
{
    IUnitOfWork UnitOfWork { get; }
}
