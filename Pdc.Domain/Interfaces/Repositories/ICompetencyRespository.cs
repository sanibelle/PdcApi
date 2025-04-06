using Pdc.Domain.Models.CourseFramework;
using Pdc.Domain.Models.MinisterialSpecification;

namespace Pdc.Domain.Interfaces.Repositories;

public interface ICompetencyRespository
{
    Task<List<MinisterialCompetencyEntity>> GetAll();
    Task<MinisterialCompetencyEntity> FindByCode(string programOfStudyCode, string competencyCode);
    Task<MinisterialCompetencyEntity> Add(ProgramOfStudy program, MinisterialCompetencyEntity entity);
    Task<MinisterialCompetencyEntity> Update(MinisterialCompetencyEntity entity);
    Task Delete(string programOfStudyCode, string competencyCode);
}
