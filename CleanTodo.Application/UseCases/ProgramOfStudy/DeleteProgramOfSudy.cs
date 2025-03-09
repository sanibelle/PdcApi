using Pdc.Application.Exceptions;
using Pdc.Domain.Interfaces.Repositories;

namespace Pdc.Application.UseCase;

public class DeleteProgramOfSudy : IDeleteProgramOfStudyUseCase
{
    private readonly IProgramOfStudyRespository _programOfStudyRespository;

    public DeleteProgramOfSudy(IProgramOfStudyRespository programOfStudyRespository)
    {
        _programOfStudyRespository = programOfStudyRespository;
    }

    public async Task Execute(Guid id)
    {
        try
        {
            await _programOfStudyRespository.FindById(id);
        }
        catch
        (Exception e)
        {
            throw new NotFoundException();
        }
        await _programOfStudyRespository.Delete(id);
    }
}