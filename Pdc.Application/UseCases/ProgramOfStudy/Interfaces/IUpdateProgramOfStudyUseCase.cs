using Pdc.Application.DTOS;

namespace Pdc.Application.UseCase;

public interface IUpdateProgramOfStudyUseCase
{
    Task<ProgramOfStudyDTO> Execute(string code, CreateProgramOfStudyDTO programOfStudy);
}