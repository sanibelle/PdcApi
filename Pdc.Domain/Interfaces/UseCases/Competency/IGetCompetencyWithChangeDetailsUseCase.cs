using Pdc.Application.DTOS;

namespace Pdc.Domain.Interfaces.UseCases.Competency;

public interface IGetCompetencyWithChangeDetailsUseCase
{
    Task<CompetencyDTO> Execute(string programOfStudyCode, string competencyCode, int changeRecordNumber);
}