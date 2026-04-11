namespace Pdc.Domain.Interfaces.UseCases.Versioning;

using Pdc.Domain.DTOS.Common;

public interface IGetComplementaryInformationUseCase
{
    Task<ComplementaryInformationDTO> Execute(Guid id);
}