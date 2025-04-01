using Pdc.Domain.Models.CourseFramework;
using Pdc.Domain.Models.MinisterialSpecification;

namespace Pdc.Domain.Interfaces.Repositories;

public interface ICompetencyRespository
{
    Task<List<MinisterialCompetency>> GetAll();
    Task<MinisterialCompetency> FindByCode(string programOfStudyCode, string competencyCode);
    Task<MinisterialCompetency> Add(ProgramOfStudy program, MinisterialCompetency entity);
    Task<MinisterialCompetency> Update(MinisterialCompetency entity);
    Task Delete(string programOfStudyCode, string competencyCode);
}
