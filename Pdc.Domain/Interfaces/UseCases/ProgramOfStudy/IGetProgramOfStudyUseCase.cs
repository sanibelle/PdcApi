using Pdc.Application.DTOS;

namespace Pdc.Domain.Interfaces.UseCases.ProgramOfStudy;

public interface IGetProgramOfStudyUseCase
{
    Task<ProgramOfStudyDTO> Execute(string code);
}