using Pdc.Application.DTOS.CourseFramework;

namespace Pdc.Domain.Interfaces.UseCases.CourseFramework;

public interface IGetCourseFrameworksByProgramOfStudyUseCase
{
    Task<IList<UnTrackedCourseFrameworkDTO>> Execute(string programOfStudyCode);
}