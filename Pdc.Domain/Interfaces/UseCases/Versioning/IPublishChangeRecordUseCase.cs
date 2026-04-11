namespace Pdc.Domain.Interfaces.UseCases.Versioning;

using Pdc.Domain.DTOS.Common;

public interface IPublishChangeRecordUseCase
{
    Task<ChangeRecordDTO> Execute(Guid id);
}