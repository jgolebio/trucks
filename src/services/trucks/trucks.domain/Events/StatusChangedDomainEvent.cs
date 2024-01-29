using Trucks.Domain.SeedWork;

namespace Trucks.Domain.Events;

public record class StatusChangedDomainEvent(Guid Id, DateTime EventDate, int StatusId, string Status) : IDomainEvent;
