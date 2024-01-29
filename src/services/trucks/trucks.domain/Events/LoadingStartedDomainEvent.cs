namespace Trucks.Domain.Events;

public record class LoadingStartedDomainEvent : StatusChangedDomainEvent
{
    public LoadingStartedDomainEvent(Guid Id, DateTime EventDate, int StatusId, string Status) : base(Id, EventDate, StatusId, Status)
    {
    }
}
