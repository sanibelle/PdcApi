using Pdc.Application.DTOS;

namespace Pdc.Domain.UseCases.ProgramOfStudy;
public interface IGetProgramOfStudiesUseCase
{
    Task<IList<ProgramOfStudyDTO>> Execute();
}