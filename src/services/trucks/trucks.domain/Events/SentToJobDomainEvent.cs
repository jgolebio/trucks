
namespace trucks.domain.Events;

public record class SentToJobDomainEvent : StatusChangedDomainEvent
{
    public SentToJobDomainEvent(Guid Id, DateTime EventDate, int StatusId, string Status) : base(Id, EventDate, StatusId, Status)
    {
    }
}
