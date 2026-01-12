using Pdc.Domain.Exceptions;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Interfaces.UseCases.Competency;
using Pdc.Infrastructure.Exceptions;

namespace Pdc.Application.UseCases;

public class DeleteCompetency : IDeleteCompetencyUseCase
{
    private readonly ICompetencyRepository _competencyRepository;

    public DeleteCompetency(ICompetencyRepository competencyRepository)
    {
        _competencyRepository = competencyRepository;
    }

    public async Task Execute(string programOfStudyCode, string competencyCode)
    {
        try
        {
            var competency = await _competencyRepository.FindByCode(competencyCode);
            if (!competency.IsDraftAndV1OrNull())
            {
                throw new InvalidOperationException("Cannot delete a non-draft competency with version greater than 1.");
            }
            await _competencyRepository.Delete(programOfStudyCode, competencyCode);
        }
        catch (EntityNotFoundException)
        {
            throw new NotFoundException();
        }
    }
}