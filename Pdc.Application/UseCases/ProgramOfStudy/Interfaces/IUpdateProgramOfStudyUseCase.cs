using Pdc.Application.DTOS;

namespace Pdc.Application.UseCases;

public interface IUpdateProgramOfStudyUseCase
{
    Task<ProgramOfStudyDTO> Execute(string code, ProgramOfStudyDTO programOfStudy);
}