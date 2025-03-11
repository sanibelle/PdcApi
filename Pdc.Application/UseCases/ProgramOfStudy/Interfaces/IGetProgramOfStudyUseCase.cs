using Pdc.Application.DTOS;

namespace Pdc.Application.UseCase;

public interface IGetProgramOfStudyUseCase
{
    Task<ProgramOfStudyDTO> Execute(Guid id);
}