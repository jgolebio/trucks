using FluentResults;
using trucks.domain.SeedWork;

namespace trucks.domain.Trucks;

public class TruckId : IdValueObject
{
    internal TruckId(Guid id) : base(id)
    {
    }

    public static Result<TruckId> Create(Guid truckId)
    {
        if (truckId == Guid.Empty)
            return Result.Fail("Truck id cannot be empty");

        return Result.Ok(new TruckId(truckId));
    }
}
