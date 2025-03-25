namespace Pdc.Application.UseCase;

public interface IDeleteProgramOfStudyUseCase
{
    Task Execute(string code);
}