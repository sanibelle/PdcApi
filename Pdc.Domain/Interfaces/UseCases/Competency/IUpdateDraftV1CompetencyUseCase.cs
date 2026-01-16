namespace Pdc.Domain.Interfaces.UseCases.Competency;
using Pdc.Application.DTOS;
using Pdc.Domain.Models.Security;

public interface IUpdateDraftV1CompetencyUseCase
{
    Task<CompetencyDTO> Execute(string programOfStudyCode, string competencyCode, CompetencyDTO updateCompetencyDto, User currentUser);
}