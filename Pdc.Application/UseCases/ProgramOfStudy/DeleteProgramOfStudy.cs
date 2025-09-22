using Pdc.Domain.Exceptions;
using Pdc.Domain.Interfaces.Repositories;

namespace Pdc.Application.UseCases;

public class DeleteProgramOfStudy : IDeleteProgramOfStudyUseCase
{
    private readonly IProgramOfStudyRepository _programOfStudyRepository;

    public DeleteProgramOfStudy(IProgramOfStudyRepository programOfStudyRepository)
    {
        _programOfStudyRepository = programOfStudyRepository;
    }

    public async Task Execute(string code)
    {
        try
        {
            await _programOfStudyRepository.FindByCode(code);
        }
        catch
        {
            throw new NotFoundException();
        }
        await _programOfStudyRepository.Delete(code);
    }
}