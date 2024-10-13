using FluentValidation;
using PointBeat.App.Entities;

namespace PointBeat.App.Validators;

public class AddressValidator : AbstractValidator<Address>
{
    public AddressValidator()
    {
        RuleFor(x => x.Street).NotNull().MinimumLength(5).MaximumLength(60);
        RuleFor(x => x.Number).NotNull();
    }
}