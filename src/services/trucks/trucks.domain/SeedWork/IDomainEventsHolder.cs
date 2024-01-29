namespace Trucks.Domain.SeedWork;

public interface IDomainEventsHolder
{
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
    void ClearDomainEvents();
}
