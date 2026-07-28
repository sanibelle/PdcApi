using Pdc.Domain.Models.CourseFramework;

namespace Pdc.Domain.Interfaces.Repositories;

public interface ICourseFrameworkRepository
{
    Task<IList<CourseFramework>> GetByProgramOfStudy(string code);
}
