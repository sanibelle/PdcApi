using Pdc.Application.DTOS;

namespace Pdc.Domain.UseCases.ProgramOfStudy;

public interface IUpdateProgramOfStudyUseCase
{
    Task<ProgramOfStudyDTO> Execute(string code, ProgramOfStudyDTO programOfStudy);
}