using FluentValidation;
using Pdc.Application.DTOS;
using Pdc.Domain.Enums;

namespace Pdc.Application.Validators;

public class ProgramOfStudyValidation : AbstractValidator<ProgramOfStudyDTO>
{
    public ProgramOfStudyValidation()
    {
        RuleFor(x => x.Code)
            .NotEmpty()
            .MaximumLength(8);

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(255);

        RuleFor(x => x.Sanction)
            .Must(s => Enum.IsDefined(typeof(SanctionType), s))
            .WithMessage("Invalid sanction type.");

        RuleFor(x => x.MonthsDuration)
            .GreaterThan(0);

        RuleFor(x => x.SpecificDurationHours)
            .GreaterThan(0);

        RuleFor(x => x.TotalDurationHours)
            .GreaterThan(0);

        RuleFor(x => x.PublishedOn)
            .NotEmpty();

        RuleFor(x => x.OptionnalUnits)
            .NotEmpty();

        RuleFor(x => x.SpecificUnits)
            .NotEmpty();
    }
}
