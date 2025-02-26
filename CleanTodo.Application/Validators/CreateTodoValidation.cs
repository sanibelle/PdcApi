using FluentValidation;
using Pdc.Application.DTOS;

namespace Pdc.Application.Validators;

public class reateProgramOfStudyValidation : AbstractValidator<CreateProgramOfStudyDto>
{
    public reateProgramOfStudyValidation()
    {
        //TODO
        //RuleFor(x => x.Title)
        //    .NotEmpty()
        //    .MaximumLength(200);
    }
}