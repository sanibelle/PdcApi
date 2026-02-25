namespace Pdc.Domain.Interfaces.UseCases.Version;

using Pdc.Domain.DTOS.Common;
using Pdc.Domain.Models.Security;

public interface IUpdateComplementaryInformationUseCase
{
    Task<ComplementaryInformationDTO> Execute(ComplementaryInformationDTO complementaryInformation, User currentUser);
}