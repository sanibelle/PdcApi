using Pdc.Application.DTOS;

namespace Pdc.Domain.Interfaces.UseCases.ProgramOfStudy;

public interface IUpdateProgramOfStudyUseCase
{
    Task<ProgramOfStudyDTO> Execute(string code, ProgramOfStudyDTO programOfStudy);
}