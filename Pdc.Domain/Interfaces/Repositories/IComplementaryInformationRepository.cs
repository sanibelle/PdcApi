using Pdc.Domain.Models.Versioning;

namespace Pdc.Domain.Interfaces.Repositories;

public interface IComplementaryInformationRepository
{
    Task<ComplementaryInformation> Add(ComplementaryInformation complementaryInformation, Guid changeableId);
    Task<Guid> FindCreatedById(Guid icomplementaryInformationId);
    Task Delete(Guid id);
    Task<ComplementaryInformation> Update(ComplementaryInformation complementaryInformation, Guid changeableId);
    Task<bool> ChangeableExists(Guid id);
    Task<Guid> GetVersionByChangeableId(Guid changeableId);
    Task<ComplementaryInformation> FindById(Guid id);
}
