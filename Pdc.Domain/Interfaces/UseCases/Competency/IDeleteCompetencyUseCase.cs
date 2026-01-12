namespace Pdc.Domain.Interfaces.UseCases.Competency;

public interface IDeleteCompetencyUseCase
{
    Task Execute(string programOfStudyCode, string competencyCode);
}