namespace Pdc.Application.UseCase;

public class DeleteProgramOfSudy : IDeleteTodoUseCase
{
    private readonly ITodoRepository _todoRepository;
    private readonly ITodoService _todoService;

    public DeleteProgramOfSudy(ITodoRepository todoRepository, ITodoService todoService)
    {
        _todoRepository = todoRepository;
        _todoService = todoService;
    }

    //public async Task Execute(Guid id)
    //{
    //    await _todoService.FindById(id);
    //    await _todoRepository.Delete(id);
    //}
}