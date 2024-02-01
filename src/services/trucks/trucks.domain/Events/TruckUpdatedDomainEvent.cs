using Trucks.Domain.SeedWork;

namespace Trucks.Domain.Events;

public record class TruckUpdatedDomainEvent(Guid Id, string Code, string Name, DateTime UpdateDate) : IDomainEvent;
