using Pdc.Application.DTOS;

namespace Pdc.Domain.Interfaces.UseCases.ProgramOfStudy;
public interface IGetProgramOfStudiesUseCase
{
    Task<IList<ProgramOfStudyDTO>> Execute();
}