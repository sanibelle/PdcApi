namespace Pdc.Domain.Interfaces.Repositories;

public interface IChangeDetailsRepository
{
    Task<List<Guid>> FindDeletedChangeableIdByChangeRecordId(Guid changeRecordId);
}
