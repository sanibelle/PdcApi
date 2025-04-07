using Pdc.Domain.Models.MinisterialSpecification;

namespace Pdc.Domain.Interfaces.Repositories
{
    public interface ICompetencyRepository
    {
        Task<List<MinisterialCompetency>> GetAll();
        Task<MinisterialCompetency> Add(MinisterialCompetency competency);
        Task<MinisterialCompetency> Update(MinisterialCompetency competency);
        Task Delete(string code);
        Task<MinisterialCompetency> FindByCode(string code);
    }
}
