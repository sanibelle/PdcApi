using Pdc.Domain.Enums;
using Pdc.Domain.Models.Versioning;

namespace Pdc.Domain.Interfaces.Repositories;

public interface IChangeRecordRepository
{
    Task<ChangeRecord> AddChangeRecord(ChangeRecord changeRecord);
    Task<ChangeRecord> FindById(Guid changeRecordId);
    Task<Guid> FindLatestChangeRecordIdByChangeRecordId(Guid rootId);
    Task<Guid> FindIdByRootIdAndNumber(int changeRecordNumber, Guid rootId);
    Task<ChangeRecord> Publish(ChangeRecord changeRecord);
    IAsyncEnumerable<ChangeDetail> FindNextChangeDetailsByChangeType(List<ChangeDetail> updatedChangeDetails, ChangeType update, Guid rootId);
    Task<List<Guid>> FindNextChangeDetailsByChangeRecordNumber(int changeRecordNumber, ChangeType add, Guid rootId);
}
