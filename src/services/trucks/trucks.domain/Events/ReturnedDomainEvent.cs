
namespace Trucks.Domain.Events;

internal record class ReturnedDomainEvent : StatusChangedDomainEvent
{
    public ReturnedDomainEvent(Guid Id, DateTime EventDate, int StatusId, string Status) : base(Id, EventDate, StatusId, Status)
    {
    }
}
