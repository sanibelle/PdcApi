using Pdc.Domain.Exceptions;
using Pdc.Domain.Interfaces.Repositories;

namespace Pdc.Application.UseCase;

public class DeleteProgramOfStudy : IDeleteProgramOfStudyUseCase
{
    private readonly IProgramOfStudyRespository _programOfStudyRespository;

    public DeleteProgramOfStudy(IProgramOfStudyRespository programOfStudyRespository)
    {
        _programOfStudyRespository = programOfStudyRespository;
    }

    public async Task Execute(string code)
    {
        try
        {
            await _programOfStudyRespository.FindByCode(code);
        }
        catch
        {
            throw new NotFoundException();
        }
        await _programOfStudyRespository.Delete(code);
    }
}