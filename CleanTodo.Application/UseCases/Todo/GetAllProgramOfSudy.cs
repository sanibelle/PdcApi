using Pdc.Domain.Interfaces.Repositories;

namespace Pdc.Application.UseCase;

public class GetAllProgramOfSudy : IGetAllProgramOfSudyUseCase
{
    private readonly IProgramOfStudyRespository _programOfStudyRespository;

    public GetAllProgramOfSudy(IProgramOfStudyRespository programOfStudyRespository)
    {
        _programOfStudyRespository = programOfStudyRespository;
    }

    public async Task<IList<TodoDto>> Execute()
    {
        var todos = await _todoRepository.GetAll();
        return todos.Select(x => new TodoDto(x)).ToList();
    }
}