using FluentResults;
using Trucks.domain.SeedWork;

namespace Trucks.domain.Trucks;

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
