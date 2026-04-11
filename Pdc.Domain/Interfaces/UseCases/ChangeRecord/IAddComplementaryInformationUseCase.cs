namespace Pdc.Domain.Interfaces.UseCases.Versioning;

using Pdc.Domain.DTOS.Common;
using Pdc.Domain.Models.Security;

public interface IAddComplementaryInformationUseCase
{
    Task<ComplementaryInformationDTO> Execute(ComplementaryInformationDTO complementaryInformation, Guid changeableId, User currentUser);
}