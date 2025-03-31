using FluentValidation;
using Pdc.Application.DTOS.Common;
using Pdc.Domain;

namespace Pdc.Application.Validators;

public class ChangeableValidation : AbstractValidator<CreateChangeableDTO>
{
    public ChangeableValidation()
    {
        RuleFor(x => x.Position)
        .NotEmpty();

        RuleFor(x => x.Value)
            .MaximumLength(Constants.MaxChangeableLength)
        .NotEmpty();

        RuleFor(x => x.Position)
            .GreaterThan(0)
        .NotEmpty();
    }
}