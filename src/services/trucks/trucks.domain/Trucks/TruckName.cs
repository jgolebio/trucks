using FluentResults;
using Trucks.domain.SeedWork;
using Trucks.domain.Trucks.Validators;

namespace Trucks.domain.Trucks
{
    public class TruckName : ValueObject
    {
        public string Value { get; }

        internal TruckName(string value)
        {
            Value = value;
        }

        public static Result<TruckName> Create(string name)
        {
            var instance = new TruckName(name);
            var validator = new TruckNameValidator();
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
}
