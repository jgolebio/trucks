using Trucks.Domain.SeedWork;

namespace Trucks.Domain.Events
{
    public record class TruckCreatedDomainEvent(Guid Id, string Code, string Name, string Description, int StatusId,
        string Status, DateTime CreateDate) : IDomainEvent;
}
