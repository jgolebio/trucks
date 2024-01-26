using FluentResults;
using Trucks.domain.SeedWork;
using Trucks.domain.Trucks.Validators;

namespace Trucks.domain.Trucks;

public class TruckCode : ValueObject
{
    public string Value { get; }

    internal TruckCode(string value)
    {
        Value = value;
    }

    public static Result<TruckCode> Create(string code)
    {
        var instance = new TruckCode(code);
        var validator = new TruckCodeValidator();
        var result = validator.Validate(instance);
        if (!result.IsValid)
            return Result.Fail(result.Errors.First().ErrorMessage);

        return Result.Ok(instance);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
