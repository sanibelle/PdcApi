namespace Pdc.Domain.Interfaces.UseCases.Versioning;

using Pdc.Domain.DTOS.Common;
using Pdc.Domain.Models.Security;

public interface IPublishChangeRecordUseCase
{
    Task<ChangeRecordDTO> Execute(Guid id, User user);
}