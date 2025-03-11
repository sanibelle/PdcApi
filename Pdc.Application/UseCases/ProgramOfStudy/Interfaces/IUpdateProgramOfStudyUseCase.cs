using Pdc.Application.DTOS;

namespace Pdc.Application.UseCase;

public interface IUpdateProgramOfStudyUseCase
{
    Task Execute(ProgramOfStudyDTO programOfStudy);
}