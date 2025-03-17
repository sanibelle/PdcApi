using FluentValidation;
using Pdc.Application.DTOS;

namespace Pdc.Application.Validators;

public class UpsertProgramOfStudyValidation : AbstractValidator<CreateProgramOfStudyDTO>
{
    public UpsertProgramOfStudyValidation()
    {
        RuleFor(x => x.Code)
            .NotEmpty()
            .MaximumLength(8);

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(255);

        RuleFor(x => x.Sanction)
            .IsInEnum();

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
