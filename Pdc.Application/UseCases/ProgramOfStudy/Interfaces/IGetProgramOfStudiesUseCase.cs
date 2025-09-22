using Pdc.Application.DTOS;

namespace Pdc.Application.UseCases;
public interface IGetProgramOfStudiesUseCase
{
    Task<IList<ProgramOfStudyDTO>> Execute();
}