using Pdc.Application.DTOS;

namespace Pdc.Domain.Interfaces.UseCases.ProgramOfStudy;

public interface ICreateProgramOfStudyUseCase
{
    Task<ProgramOfStudyDTO> Execute(ProgramOfStudyDTO programOfStudy);
}