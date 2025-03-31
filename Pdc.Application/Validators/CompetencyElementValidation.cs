using FluentValidation;
using Pdc.Application.DTOS.Common;

namespace Pdc.Application.Validators;

public class CompetencyElementValidation : AbstractValidator<CreateCompetencyElementDTO>
{
    public CompetencyElementValidation()
    {
        Include(new ChangeableValidation());

        // Add any additional rules specific to CreateCompetencyElementDTO
        RuleFor(x => x.PerformanceCriterias)
            .ForEach(y => y.SetValidator(new ChangeableValidation()))
            .NotEmpty();
        // TODO FR .WithMessage("PerformanceCriterias cannot be empty.");

        RuleForEach(x => x.ComplementaryInformations)
            .SetValidator(new ComplementaryInformationValidator())
            .NotEmpty();
    }
}
