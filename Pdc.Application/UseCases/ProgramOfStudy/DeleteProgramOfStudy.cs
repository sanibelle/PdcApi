using Pdc.Application.Exceptions;
using Pdc.Domain.Interfaces.Repositories;

namespace Pdc.Application.UseCase;

public class DeleteProgramOfStudy : IDeleteProgramOfStudyUseCase
{
    private readonly IProgramOfStudyRespository _programOfStudyRespository;

    public DeleteProgramOfStudy(IProgramOfStudyRespository programOfStudyRespository)
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
        {
            throw new NotFoundException();
        }
        await _programOfStudyRespository.Delete(id);
    }
}