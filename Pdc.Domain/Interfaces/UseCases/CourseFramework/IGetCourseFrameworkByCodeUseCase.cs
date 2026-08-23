using Pdc.Application.DTOS.CourseFramework;

namespace Pdc.Domain.Interfaces.UseCases.CourseFramework;

public interface IGetCourseFrameworkByCodeUseCase
{
    Task<UntrackedCourseFrameworkDTO> Execute(string coursFrameworkCode);
}