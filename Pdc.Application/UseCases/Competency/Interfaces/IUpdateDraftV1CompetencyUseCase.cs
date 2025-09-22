namespace Pdc.Application.UseCases;
using Pdc.Application.DTOS;

public interface IUpdateDraftV1CompetencyUseCase
{
    Task<CompetencyDTO> Execute(string programOfStudyCode, string competencyCode, CompetencyDTO updateCompetencyDto);
}