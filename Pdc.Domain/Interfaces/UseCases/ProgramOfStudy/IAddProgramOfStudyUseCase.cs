using Pdc.Application.DTOS;

namespace Pdc.Domain.Interfaces.UseCases.ProgramOfStudy;

public interface IAddProgramOfStudyUseCase
{
    Task<ProgramOfStudyDTO> Execute(ProgramOfStudyDTO programOfStudy);
}