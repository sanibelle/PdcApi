namespace Pdc.Domain.Interfaces.UseCases.Competency;

using Pdc.Application.DTOS;
using Pdc.Domain.Models.Security;

public interface IUpdatePublishedCompetencyUseCase
{
    Task<CompetencyDTO> Execute(string programOfStudyCode, string competencyCode, CompetencyDTO updateCompetencyDto, User currentUser);
}