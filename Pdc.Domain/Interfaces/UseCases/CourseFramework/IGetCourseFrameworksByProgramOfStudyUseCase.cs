using Pdc.Application.DTOS;

namespace Pdc.Domain.Interfaces.UseCases.ProgramOfStudy;

public interface IGetCourseFrameworksByProgramOfStudyUseCase
{
    Task<IList<CourseFrameworkDTO>> Execute(string programOfStudyCode);
}