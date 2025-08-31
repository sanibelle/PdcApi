namespace Pdc.Application.UseCase;
using Pdc.Application.DTOS;

public interface IUpdateDraftV1CompetencyUseCase
{
    Task<CompetencyDTO> Execute(string programOfStudyCode, CompetencyDTO updateCompetencyDto);
}