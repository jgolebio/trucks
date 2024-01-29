using FluentResults;
using Trucks.Domain.SeedWork;

namespace Trucks.Domain.Trucks;

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
