using Pdc.Domain.Models.Versioning;

namespace Pdc.Domain.Interfaces.Repositories;

public interface IChangeRecordRepository
{
    Task<ChangeRecord> AddChangeRecord(ChangeRecord changeRecord);
    Task<ChangeRecord> FindById(Guid changeRecordId);
    Task<Guid> FindParentByChangeRecordId(Guid changeRecordId);
    Task<Guid> FindIdByParentIdAndNumber(int changeRecordNumber, Guid ParentChangeRecordId);
    Task<ChangeRecord> Publish(ChangeRecord changeRecord);
}
