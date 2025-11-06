namespace Pdc.Domain.UseCases.ProgramOfStudy;

public interface IDeleteProgramOfStudyUseCase
{
    Task Execute(string code);
}