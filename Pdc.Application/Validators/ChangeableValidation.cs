using FluentValidation;
using Pdc.Application.DTOS.Common;
using Pdc.Domain;

namespace Pdc.Application.Validators;

public class ChangeableValidation : AbstractValidator<ChangeableDTO>
{
    public ChangeableValidation()
    {
        RuleFor(x => x.Value)
            .MaximumLength(Constants.MaxChangeableLength)
        .NotEmpty();

        // TODO valider que les positions sont de 1 à n sans sauts.
        RuleFor(x => x.Position)
            .Custom((x, context) =>
            {
                if (x.HasValue && x.Value <= 0)
                {
                    context.AddFailure("Position", "Position must be greater than 0.");
                }
            });
    }
}