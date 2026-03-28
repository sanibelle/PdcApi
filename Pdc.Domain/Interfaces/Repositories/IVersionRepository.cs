using Pdc.Domain.Models.Versioning;

namespace Pdc.Domain.Interfaces.Repositories;

public interface IVersionRepository
{
    Task<ChangeRecord> AddVersion(ChangeRecord version);
    Task<Guid> FindParentByVersionId(Guid versionId);
}
