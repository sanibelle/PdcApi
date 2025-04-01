using FluentValidation;
using Pdc.Application.DTOS.Common;
using Pdc.Domain;

namespace Pdc.Application.Validators;

public class ComplementaryInformationValidator : AbstractValidator<ComplementaryInformationDTO>
{
    public ComplementaryInformationValidator()
    {
        RuleFor(x => x.Text)
            .MaximumLength(Constants.MaxComplementaryInformationsLength)
            .NotEmpty();
    }
}
