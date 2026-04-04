namespace Pdc.Domain.Interfaces.UseCases.ChangeRecord;

using Pdc.Domain.DTOS.Common;

public interface IGetComplementaryInformationUseCase
{
    Task<ComplementaryInformationDTO> Execute(Guid id);
}