using Pdc.Application.DTOS;
using Pdc.Application.Services.Competency;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Interfaces.UseCases.Competency;
using Pdc.Domain.Models.MinisterialSpecification;

namespace Pdc.Application.UseCases;

public class GetCompetency(ICompetencyRepository competencyRepository, CompetencyService competencyService) : IGetCompetencyUseCase
{
    public async Task<CompetencyDTO> Execute(string programOfStudyCode, string competencyCode)
    {
        MinisterialCompetency competency = await competencyRepository.FindByCode(competencyCode);
        return await competencyService.RemoveDeletedChangeables(competency);
    }
}
