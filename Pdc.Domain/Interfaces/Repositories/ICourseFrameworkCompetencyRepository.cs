namespace Pdc.Domain.Interfaces.Repositories;

public interface ICourseFrameworkCompetencyRepository
{
    Task Link(string competencyCode, Guid courseFrameworkId);
    Task Unlink(string competencyCode, Guid courseFrameworkId);
}
