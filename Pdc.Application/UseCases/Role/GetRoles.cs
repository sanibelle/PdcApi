using AutoMapper;
using Pdc.Domain.DTOS.Common;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Interfaces.UseCases.Role;
using Pdc.Domain.Interfaces.UseCases.User;
using Pdc.Domain.Models.Security;

namespace Pdc.Application.UseCases;

public class GetRoles(IUserRepository roleRepository) : IGetRolesUseCase
{
    private readonly IUserRepository _roleRepository = roleRepository;

    public async Task<IList<string>> Execute()
    {
        return await _roleRepository.GetAllRoles();
    }
}
