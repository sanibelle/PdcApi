using Pdc.Application.DTOS.Common;
using Pdc.Domain.Models.Security;

public interface IGetUserUseCase
{
    Task<UserDTO> Execute(Guid UserId);
}