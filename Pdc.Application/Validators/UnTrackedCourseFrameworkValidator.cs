using FluentValidation;
using Pdc.Application.DTOS.CourseFramework;

namespace Pdc.Application.Validators;

public class UnTrackedCourseFrameworkValidator : AbstractValidator<UnTrackedCourseFrameworkDTO>
{
    public UnTrackedCourseFrameworkValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty()
            .MaximumLength(11);

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(255);

        RuleFor(x => x.TheoryHours)
            .NotEmpty()
            .LessThan(25)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.LaboratoryHours)
            .NotEmpty()
            .LessThan(25)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.PersonnalWorkHours)
            .NotEmpty()
            .LessThan(25)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.Semester)
            .NotEmpty()
            .LessThan(10)
            .GreaterThanOrEqualTo(1);
    }
}
