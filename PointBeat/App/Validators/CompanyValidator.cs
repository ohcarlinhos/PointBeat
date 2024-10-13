using FluentValidation;
using PointBeat.App.Entities;

namespace PointBeat.App.Validators;

public class CompanyValidator : AbstractValidator<Company>
{
    public CompanyValidator()
    {
        RuleFor(x => x.Name).NotNull().Length(5, 60);
        RuleFor(x => x.Address).NotNull().SetValidator(new AddressValidator());
    }
}