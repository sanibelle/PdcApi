using FluentValidation;
using Pdc.Application.DTOS;

namespace Pdc.Application.Validators;

public class CompetencyValidation : AbstractValidator<CompetencyDTO>
{
    public CompetencyValidation()
    {
        RuleFor(x => x.Code)
            .NotEmpty()
            .MaximumLength(8);

        RuleFor(x => x.StatementOfCompetency)
            .NotEmpty()
            .MaximumLength(500);

        RuleFor(x => x.RealisationContexts)
            .NotEmpty();

        RuleFor(x => x.CompetencyElements)
            .ForEach(y => y.SetValidator(new CompetencyElementValidation()))
            .NotEmpty();
    }
}
