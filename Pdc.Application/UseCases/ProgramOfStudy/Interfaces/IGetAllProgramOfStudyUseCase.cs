using Pdc.Application.DTOS;

namespace Pdc.Application.UseCase;
public interface IGetAllProgramOfStudyUseCase
{
    Task<IList<ProgramOfStudyDTO>> Execute();
}