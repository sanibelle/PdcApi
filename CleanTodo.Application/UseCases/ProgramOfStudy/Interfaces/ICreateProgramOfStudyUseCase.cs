using Pdc.Application.DTOS;

namespace Pdc.Application.UseCase;

public interface ICreateProgramOfStudyUseCase
{
    Task<ProgramOfStudyDTO> Execute(CreateProgramOfStudyDTO createTodoDto);
}