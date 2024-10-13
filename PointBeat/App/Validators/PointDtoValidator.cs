using FluentValidation;
using PointBeat.App.Dtos;

namespace PointBeat.App.Validators;

public class PointDtoValidator : AbstractValidator<PointDto>
{
    public PointDtoValidator()
    {
        RuleFor(x => x.Hour).NotNull().Must(ValidatorUtils.BeAValidDate)
            .WithMessage("'Hour' é uma data inválida.");
        RuleFor(x => x.UserId).NotNull().NotEmpty();
        RuleFor(x => x.CompanyId).NotNull().NotEmpty();
    }
}