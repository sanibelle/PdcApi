using Pdc.Application.DTOS;

namespace Pdc.Application.UseCases;

public interface ICreateProgramOfStudyUseCase
{
    Task<ProgramOfStudyDTO> Execute(ProgramOfStudyDTO programOfStudy);
}