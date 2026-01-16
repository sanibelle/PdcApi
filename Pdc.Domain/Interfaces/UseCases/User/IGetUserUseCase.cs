using Pdc.Domain.DTOS.Common;

namespace Pdc.Domain.Interfaces.UseCases.User;

public interface IGetUserUseCase
{
    Task<UserDTO> Execute(Guid userId);
}