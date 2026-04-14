using Pdc.Domain.Models.Versioning;

namespace Pdc.Domain.Interfaces.Repositories;

public interface IChangeableRepository
{
    Task<Changeable> FindById(Guid id);
    Task<Changeable> Update(Changeable changeable);
}
