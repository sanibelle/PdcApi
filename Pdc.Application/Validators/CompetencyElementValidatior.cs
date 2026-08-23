using FluentValidation;
using Pdc.Application.DTOS.Common;

namespace Pdc.Application.Validators;

public class CompetencyElementValidatior : AbstractValidator<CompetencyElementDTO>
{
    public CompetencyElementValidatior()
    {
        Include(new ChangeableValidatior());

        RuleFor(x => x.PerformanceCriterias)
            .NotEmpty()
            .WithMessage("Le critère de performance ne peut être vide")
            .ForEach(y => y.SetValidator(new ChangeableValidatior()));
    }
}
