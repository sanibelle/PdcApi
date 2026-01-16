using Pdc.Domain.Exceptions;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Interfaces.UseCases.ProgramOfStudy;

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
        await _programOfStudyRepository.Delete(code);
    }
}