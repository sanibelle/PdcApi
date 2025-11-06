using Pdc.Application.DTOS;

namespace Pdc.Domain.UseCases.Competency;

public interface IGetCompetencyUseCase
{
    Task<CompetencyDTO> Execute(string programOfStudyCode, string competencyCode);
}