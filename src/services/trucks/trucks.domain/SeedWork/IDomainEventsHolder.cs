namespace trucks.domain.SeedWork;

public interface IDomainEventsHolder
{
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
    void ClearDomainEvents();
}
