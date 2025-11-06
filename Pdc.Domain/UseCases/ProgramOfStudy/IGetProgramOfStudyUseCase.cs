using Pdc.Application.DTOS;

namespace Pdc.Domain.UseCases.ProgramOfStudy;

public interface IGetProgramOfStudyUseCase
{
    Task<ProgramOfStudyDTO> Execute(string code);
}