using FluentValidation;
using PointBeat.App.Entities;

namespace PointBeat.App.Validators;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(x => x.Name).NotNull().Length(3, 60);
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.Address).SetValidator(new AddressValidator());
    }
}