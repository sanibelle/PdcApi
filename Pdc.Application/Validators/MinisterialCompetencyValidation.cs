using FluentValidation;
using Pdc.Application.DTOS;

namespace Pdc.Application.Validators;

public class CompetencyValidation : AbstractValidator<CreateCompetencyDTO>
{
    public CompetencyValidation()
    {
        RuleFor(x => x.Code)
            .NotEmpty()
            .MaximumLength(8);
    }
}
