using FluentResults;
using Trucks.Domain.SeedWork;

namespace Trucks.Domain.Trucks;

public class TruckDescription : ValueObject
{
    public string Value { get; }

    internal TruckDescription(string value)
    {
        Value = value;
    }

    public static Result<TruckDescription> Create(string description) =>
        Result.Ok(new TruckDescription(description));

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
