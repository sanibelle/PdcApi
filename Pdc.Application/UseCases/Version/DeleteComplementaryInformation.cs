using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Interfaces.UseCases.Version;
using Pdc.Domain.Models.Security;

namespace Pdc.Application.UseCases.Version;

public class DeleteComplementaryInformation(IComplementaryInformationRepository complementaryInformationRepository) : IDeleteComplementaryInformationUseCase
{
    private readonly IComplementaryInformationRepository _complementaryInformationRepository = complementaryInformationRepository;

    public async Task Execute(Guid id, User currentUser)
    {
        Guid userId = await _complementaryInformationRepository.FindCreatedById(id);
        if (currentUser.Id != userId && !currentUser.IsAdmin)
        {
            throw new UnauthorizedAccessException();
        }
        await _complementaryInformationRepository.Delete(id);
    }
}