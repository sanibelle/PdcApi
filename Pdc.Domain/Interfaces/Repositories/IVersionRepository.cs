namespace Pdc.Domain.Interfaces.Repositories;

public interface IVersionRepository
{
    Task<Guid> FindParentByVersionId(Guid versionId);
}
