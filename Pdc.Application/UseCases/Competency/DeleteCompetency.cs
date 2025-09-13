using Pdc.Domain.Exceptions;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Infrastructure.Exceptions;

namespace Pdc.Application.UseCase;

public class DeleteCompetency : IDeleteCompetencyUseCase
{
    private readonly ICompetencyRepository _competencyRespository;

    public DeleteCompetency(ICompetencyRepository competencyRespository)
    {
        _competencyRespository = competencyRespository;
    }

    public async Task Execute(string programOfStudyCode, string competencyCode)
    {
        try
        {
            var competency = await _competencyRespository.FindByCode(programOfStudyCode, competencyCode);
            if (!competency.IsDraftAndV1OrNull())
            {
                throw new InvalidOperationException("Cannot delete a non-draft competency with version greater than 1.");
            }
        }
        catch (EntityNotFoundException)
        {
            throw new NotFoundException();
        }
        await _competencyRespository.Delete(programOfStudyCode, competencyCode);
    }
}