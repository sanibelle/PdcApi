using Pdc.Domain.Interfaces.Repositories;

namespace Pdc.Application.UseCase;

public class CreateProgramOfSudy : IProgramOfStudyUseCase
{
    private readonly IProgramOfStudyRespository _programOfStudyRespository;

    public CreateProgramOfSudy(IProgramOfStudyRespository programOfStudyRespository)
    {
        _programOfStudyRespository = programOfStudyRespository;
    }

    //public async Task<TodoDto> Execute(CreateTodoDto createTodoDto)
    //{
    //    var todo = new Todo(
    //        createTodoDto.Title
    //    );

    //    var savedTodo = await _todoRepository.Add(todo);

    //    return new TodoDto(savedTodo);
    //}
}