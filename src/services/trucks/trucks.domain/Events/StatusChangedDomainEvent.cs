using trucks.domain.SeedWork;

namespace trucks.domain.Events;

public record class StatusChangedDomainEvent(Guid Id, DateTime EventDate, int StatusId, string Status) : IDomainEvent;
