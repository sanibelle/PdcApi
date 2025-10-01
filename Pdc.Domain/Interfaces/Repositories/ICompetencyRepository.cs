using Pdc.Domain.Models.CourseFramework;
using Pdc.Domain.Models.MinisterialSpecification;

namespace Pdc.Domain.Interfaces.Repositories;

public interface ICompetencyRepository
{
    Task<List<MinisterialCompetency>> GetAll();
    Task<MinisterialCompetency> FindByCode(string competencyCode);
    Task<MinisterialCompetency> Update(MinisterialCompetency entity);
    Task Delete(string programOfStudyCode, string competencyCode);
    Task<bool> ExistsEntityByCode(string programOfStudyCode, string competencyCode);
    Task<MinisterialCompetency> Add(ProgramOfStudy program, MinisterialCompetency competency);
    Task<List<MinisterialCompetency>> GetByProgramOfStudy(string programOfStudyCode);
}
