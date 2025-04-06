using Pdc.Domain.Models.MinisterialSpecification;

namespace Pdc.Domain.Interfaces.Repositories
{
    public interface ICompetencyRepository
    {
        Task<List<MinisterialCompetencyEntity>> GetAll();
        Task<MinisterialCompetencyEntity> Add(MinisterialCompetencyEntity competency);
        Task<MinisterialCompetencyEntity> Update(MinisterialCompetencyEntity competency);
        Task Delete(string code);
        Task<MinisterialCompetencyEntity> FindByCode(string code);
    }
}
