using Pdc.Domain.DTOS.Common;

namespace Pdc.Domain.Interfaces.UseCases.Role;

public interface IGetRolesUseCase
{
    Task<IList<string>> Execute();
}