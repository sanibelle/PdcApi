using FluentValidation;
using FluentValidation.Results;
using Pdc.Application.DTOS;
using Pdc.Application.DTOS.Common;

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
            .ForEach(y => y.SetValidator(new ChangeableValidation()))
            .When(x => x.RealisationContexts is not null);

        RuleFor(x => x.CompetencyElements)
            .Custom((x, context) => ThrowIfPositionsInvalid(x, context))
            .ForEach(y => y.SetValidator(new CompetencyElementValidation()));
    }


    private void ThrowIfPositionsInvalid(ICollection<CompetencyElementDTO> competencyElements, ValidationContext<CompetencyDTO> context)
    {
        if (competencyElements == null || competencyElements.Count == 0)
        {
            return;
        }
        ThrowIfPositionsInvalid(competencyElements.Cast<ChangeableDTO>().ToList(), context);
        foreach (var competencyElement in competencyElements)
        {
            ThrowIfPositionsInvalid(competencyElement.PerformanceCriterias, context);
        }
    }

    private static void ThrowIfPositionsInvalid(ICollection<ChangeableDTO> dtos, ValidationContext<CompetencyDTO> context)
    {
        // starts at 1
        for (int i = 1; i <= dtos.Count; i++)
        {
            ChangeableDTO? dto = dtos.FirstOrDefault(x => x.Position == i);
            if (dto == null)
            {
                context.AddFailure(new ValidationFailure(dtos.First().GetType().Name, $"Could not find expected position: {i}"));
            }
        }
    }
}
