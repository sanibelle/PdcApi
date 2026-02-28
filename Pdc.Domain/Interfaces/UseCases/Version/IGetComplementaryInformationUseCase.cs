namespace Pdc.Domain.Interfaces.UseCases.Version;

using Pdc.Domain.DTOS.Common;

public interface IGetComplementaryInformationUseCase
{
    Task<ComplementaryInformationDTO> Execute(Guid id);
}