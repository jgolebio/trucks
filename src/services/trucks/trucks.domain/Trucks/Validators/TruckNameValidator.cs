using FluentValidation;

namespace Trucks.domain.Trucks.Validators;

internal class TruckNameValidator : AbstractValidator<TruckName>
{
    public TruckNameValidator()
    {
        RuleFor(x => x.Value).NotEmpty().WithMessage("Truck name cannot be empty");
    }
}
