using Pdc.Application.DTOS;

namespace Pdc.Application.UseCase;
public interface IGetProgramOfStudiesUseCase
{
    Task<IList<ProgramOfStudyDTO>> Execute();
}