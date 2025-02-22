using Pdc.Application.DTOS;

namespace Pdc.Application.Service.Todo;

public interface ITodoService
{
    public Task<TodoDto> FindById(Guid id);

}
