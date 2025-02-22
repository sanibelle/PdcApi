using Pdc.Application.DTOS;

namespace Pdc.Application.UseCase;

public interface ICreateTodoUseCase
{
    Task<TodoDto> Execute(CreateTodoDto createTodoDto);
}