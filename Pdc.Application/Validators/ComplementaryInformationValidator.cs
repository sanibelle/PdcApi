using FluentValidation;
using Pdc.Application.DTOS.Common;
using Pdc.Domain;

namespace Pdc.Application.Validators;

public class ComplementaryInformationValidator : AbstractValidator<CreateComplementaryInformationDTO>
{
    public ComplementaryInformationValidator()
    {
        RuleFor(x => x.Text)
            .MaximumLength(Constants.MaxComplementaryInformationsLength)
            .NotEmpty();
    }
}
