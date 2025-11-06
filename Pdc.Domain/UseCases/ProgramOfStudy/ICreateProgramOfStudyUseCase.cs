using Pdc.Application.DTOS;

namespace Pdc.Domain.UseCases.ProgramOfStudy;

public interface ICreateProgramOfStudyUseCase
{
    Task<ProgramOfStudyDTO> Execute(ProgramOfStudyDTO programOfStudy);
}