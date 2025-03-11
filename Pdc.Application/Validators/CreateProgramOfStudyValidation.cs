using FluentValidation;
using Pdc.Application.DTOS;

namespace Pdc.Application.Validators;

public class CreateProgramOfStudyValidation : AbstractValidator<CreateProgramOfStudyDTO>
{
    public CreateProgramOfStudyValidation()
    {
        //TODO
        //RuleFor(x => x.Title)
        //    .NotEmpty()
        //    .MaximumLength(200);
    }
}