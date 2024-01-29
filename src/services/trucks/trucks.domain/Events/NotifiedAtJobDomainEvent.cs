
namespace Trucks.Domain.Events;

public record class NotifiedAtJobDomainEvent : StatusChangedDomainEvent 
{
    public NotifiedAtJobDomainEvent(Guid Id, DateTime EventDate, int StatusId, string Status) : base(Id, EventDate, StatusId, Status)
    {
    }
}
