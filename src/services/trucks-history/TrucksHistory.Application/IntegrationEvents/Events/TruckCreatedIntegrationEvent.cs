using EventBus.Events;

namespace TrucksHistory.Application.IntegrationEvents.Events;

public record class TruckCreatedIntegrationEvent : IntegrationEvent
{
    public Guid TruckId { get; }
    public string TruckCode { get; }
    public string TruckName { get; }
    public int StatusId { get; }
    public string Status { get; }

    public TruckCreatedIntegrationEvent(Guid id, DateTime createDate, Guid truckId, string truckCode,
        string truckName, int statusId, string status) : base(id, createDate)
    {
        TruckId = truckId;
        TruckCode = truckCode;
        TruckName = truckName;
        StatusId = statusId;
        Status = status;
    }
}
