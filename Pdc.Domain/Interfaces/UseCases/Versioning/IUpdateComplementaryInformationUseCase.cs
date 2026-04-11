namespace Pdc.Domain.Interfaces.UseCases.Versioning;

using Pdc.Domain.DTOS.Common;
using Pdc.Domain.Models.Security;
using System;

public interface IUpdateComplementaryInformationUseCase
{
    Task<ComplementaryInformationDTO> Execute(ComplementaryInformationDTO complementaryInformation, User currentUser, Guid complementaryInformationId);
}