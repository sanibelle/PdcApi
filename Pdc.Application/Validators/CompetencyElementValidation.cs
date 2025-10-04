using FluentValidation;
using Pdc.Application.DTOS.Common;

namespace Pdc.Application.Validators;

public class CompetencyElementValidation : AbstractValidator<CompetencyElementDTO>
{
    public CompetencyElementValidation()
    {
        Include(new ChangeableValidation());

        // Add any additional rules specific to CreateCompetencyElementDTO
        RuleFor(x => x.PerformanceCriterias)
            .NotEmpty()
            .WithMessage("Le critère de performance ne peut être vide")
            .ForEach(y => y.SetValidator(new ChangeableValidation()));
    }

}

