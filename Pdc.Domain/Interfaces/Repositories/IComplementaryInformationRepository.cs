using Pdc.Domain.Models.Versioning;

namespace Pdc.Domain.Interfaces.Repositories;

public interface IComplementaryInformationRepository
{
    Task<ComplementaryInformation> Add(ComplementaryInformation complementaryInformation, Guid changeRecordId, Guid ChangeableId);
    Task<Guid> FindCreatedByByComplementaryInformationId(Guid complementaryInformationId);
    Task Delete(Guid id);
    Task<ComplementaryInformation> Update(ComplementaryInformation complementaryInformation, Guid changeableId);
    Task<bool> ChangeableExists(Guid id);
    Task<Guid> GetChangeRecordByChangeableId(Guid changeRecordId);
    Task<ComplementaryInformation> FindById(Guid id);
}
