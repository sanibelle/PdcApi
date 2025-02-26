using Pdc.Domain.Entities.CourseFramework;

namespace Pdc.Application.UseCase;
public interface IGetAllProgramOfSudyUseCase
{
    Task<IList<ProgramOfStudy>> Execute();
}