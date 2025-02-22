using Pdc.Application.DTOS;

namespace Pdc.Application.UseCase;
public interface IGetAllTodosUseCase
{
    Task<IList<TodoDto>> Execute();
}