using Pdc.Application.DTOS;

namespace Pdc.Application.UseCase;

public interface IUpdateProgramOfStudyUseCase
{
    Task<ProgramOfStudyDTO> Execute(Guid id, UpsertProgramOfStudyDTO programOfStudy);
}