using Pdc.Application.DTOS;

namespace Pdc.Application.UseCases;

public interface IGetProgramOfStudyUseCase
{
    Task<ProgramOfStudyDTO> Execute(string code);
}