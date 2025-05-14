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

        RuleFor(x => x.ProgramType)
            .Must(s => Enum.IsDefined(typeof(ProgramType), s))
            .WithMessage("Invalid program type.");

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
