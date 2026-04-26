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
        if (!competency.ChangeRecord.Id.HasValue)
        {
            throw new NullReferenceException("Competency must have a valid ChangeRecord with an Id.");
        }
        return await competencyService.RemoveDeletedChangeables(competency, competency.ChangeRecord.Id.Value);
    }
}
