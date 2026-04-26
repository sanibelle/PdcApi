using Pdc.Domain.Models.Versioning;

namespace Pdc.Domain.Interfaces.Repositories;

public interface IChangeDetailsRepository
{
    Task<List<Guid>> FindDeletedChangeableIdByChangeRecordId(Guid changeRecordId, int? recordNumberToSkip = null);
    Task<List<ChangeDetail>> GetChangeDetailsByChangeRecordId(Guid changeRecordId);
}
