using Pdc.Application.DTOS;

namespace Pdc.Application.UseCase;
public interface IGetAllCompetencyUseCase
{
    Task<IList<CompetencyDTO>> Execute();
}