using FluentValidation;
using Pdc.Application.DTOS.CourseFramework;

namespace Pdc.Application.Validators;

public class CreateCourseFrameworkValidator : AbstractValidator<CreateCourseFrameworkDTO>
{
    public CreateCourseFrameworkValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty()
            .MaximumLength(11);

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(255);

        RuleFor(x => x.TheoryHours)
            .LessThan(25)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.LaboratoryHours)
            .LessThan(25)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.PersonnalWorkHours)
            .LessThan(25)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.Semester)
            .NotEmpty()
            .LessThan(10)
            .GreaterThanOrEqualTo(1);
    }
}
