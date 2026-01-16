using FluentValidation;
using Pdc.Domain;
using Pdc.Domain.DTOS.Common;

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
