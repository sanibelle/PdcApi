using FluentValidation;
using Pdc.Domain;
using Pdc.Domain.DTOS.Common;

namespace Pdc.Application.Validators;

public class ChangeableValidation : AbstractValidator<ChangeableDTO>
{
    public ChangeableValidation()
    {
        RuleFor(x => x.Value)
            .MaximumLength(Constants.MaxChangeableLength)
        .NotEmpty();

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