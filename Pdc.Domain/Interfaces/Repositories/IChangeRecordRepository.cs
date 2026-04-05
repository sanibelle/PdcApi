using Pdc.Domain.Models.Versioning;

namespace Pdc.Domain.Interfaces.Repositories;

public interface IChangeRecordRepository
{
    Task<ChangeRecord> AddChangeRecord(ChangeRecord changeRecord);
    Task<Guid> FindParentByChangeRecordId(Guid changeRecordId);
}
