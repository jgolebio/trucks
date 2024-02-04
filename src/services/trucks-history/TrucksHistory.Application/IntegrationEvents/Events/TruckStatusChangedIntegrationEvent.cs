using EventBus.Events;

namespace TrucksHistory.Application.IntegrationEvents.Events;

public record class TruckStatusChangedIntegrationEvent : IntegrationEvent
{
    public Guid TruckId { get; }
    public string Status { get; }
    public int StatusId { get; }
    public DateTime EventDate { get; }

    public TruckStatusChangedIntegrationEvent(Guid id, Guid truckId, string status, int statusId, DateTime eventDate)
        : base(id, eventDate)
    {
        TruckId = truckId;
        Status = status;
        StatusId = statusId;
        EventDate = eventDate;
    }
}
