
namespace Trucks.Domain.Events;

public record class WentOutOfServiceDomainEvent : StatusChangedDomainEvent
{
    public WentOutOfServiceDomainEvent(Guid Id, DateTime EventDate, int StatusId, string Status) : base(Id, EventDate, StatusId, Status)
    {
    }
}
