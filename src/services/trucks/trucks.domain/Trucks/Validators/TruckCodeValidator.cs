using FluentValidation;

namespace Trucks.domain.Trucks.Validators;

internal class TruckCodeValidator : AbstractValidator<TruckCode>
{
    public TruckCodeValidator()
    {
        RuleFor(x => x.Value).NotEmpty();
        RuleFor(x => x.Value).Matches("^[a-zA-Z0-9]*$").WithMessage("Truck code should be alphanumeric");
    }
}
