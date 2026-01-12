namespace Pdc.Domain.Interfaces.UseCases.ProgramOfStudy;

public interface IDeleteProgramOfStudyUseCase
{
    Task Execute(string code);
}