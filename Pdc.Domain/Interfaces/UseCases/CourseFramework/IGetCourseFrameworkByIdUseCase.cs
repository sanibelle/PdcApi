using Pdc.Application.DTOS.CourseFramework;

namespace Pdc.Domain.Interfaces.UseCases.CourseFramework;

public interface IGetCourseFrameworkByIdUseCase
{
    Task<TrackedCourseFrameworkDTO> Execute(Guid id);
}