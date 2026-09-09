using FluentValidation;
using Pdc.Application.DTOS.CourseFramework;

namespace Pdc.Application.Validators;

public class TrackedCourseFrameworkValidator : AbstractValidator<TrackedCourseFrameworkDTO>
{
    public TrackedCourseFrameworkValidator()
    {
        RuleFor(x => x.Code.Value)
            .NotEmpty()
            .MaximumLength(11);

        RuleFor(x => x.Name.Value)
            .NotEmpty()
            .MaximumLength(255);

        RuleFor(x => x.Weighting.TheoryHours.Value)
            .NotEmpty()
            .LessThan(25)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.Weighting.LaboratoryHours.Value)
            .NotEmpty()
            .LessThan(25)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.Weighting.PersonnalWorkHours.Value)
            .NotEmpty()
            .LessThan(25)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.Semester.Value)
            .NotEmpty()
            .LessThan(10)
            .GreaterThanOrEqualTo(1);
    }
}
