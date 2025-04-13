namespace Pdc.Application.UseCase;
using Pdc.Application.DTOS;
using Pdc.Domain.Models.Security;

public interface ICreateCompetencyUseCase
{
    Task<CompetencyDTO> Execute(string programOfStudyCode, CompetencyDTO programOfStudy, User currentUser);
}