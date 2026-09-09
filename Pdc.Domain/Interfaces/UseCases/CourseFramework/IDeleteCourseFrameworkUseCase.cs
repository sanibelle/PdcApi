namespace Pdc.Domain.Interfaces.UseCases.CourseFramework;

public interface IDeleteCourseFrameworkUseCase
{
    Task Execute(Guid coursFrameworkId);
}