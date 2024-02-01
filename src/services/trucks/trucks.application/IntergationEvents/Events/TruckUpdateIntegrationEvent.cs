using EventBus.Events;

namespace Trucks.Application.IntergationEvents.Events;

public record class TruckUpdateIntegrationEvent : IntegrationEvent
{
    public Guid TruckId { get; }
    public string TruckCode { get; }
    public string TruckName { get; }

    public TruckUpdateIntegrationEvent(Guid id, DateTime createDate, Guid truckId, string truckCode,
        string truckName) : base(id, createDate)
    {
        TruckId = truckId;
        TruckCode = truckCode;
        TruckName = truckName;
    }
}
