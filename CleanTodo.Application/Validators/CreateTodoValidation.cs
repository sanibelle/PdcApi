using Pdc.Application.DTOS;
using FluentValidation;

namespace Pdc.Application.Validators;

// Valide automatiquement CreateTodoDto quand il est créé dans le controller
public class CreateTodoValidation : AbstractValidator<CreateTodoDto>
{
    public CreateTodoValidation()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(200);
    }
}