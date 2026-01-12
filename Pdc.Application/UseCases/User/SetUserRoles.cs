using AutoMapper;
using Pdc.Application.DTOS.Common;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Interfaces.UseCases.User;
using Pdc.Domain.Models.Security;

namespace Pdc.Application.UseCases;

public class SetUserRoles : ISetUserRolesUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public SetUserRoles(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserDTO> Execute(Guid userId, string[] roles)
    {
        IList<string> userRoles = await _userRepository.FindUserRolesByUserId(userId);
        List<string> rolesToAdd = roles.Where(x => !userRoles.Any(r => r == x)).ToList();
        List<string> rolesToRemove = userRoles.Where(x => !roles.Any(r => r == x)).ToList();
        if (rolesToAdd.Any())
        {
            await _userRepository.AddUserRoles(userId, rolesToAdd);
        }
        if (rolesToRemove.Any())
        {
            await _userRepository.RemoveUserRoles(userId, rolesToRemove);
        }
        User user = await _userRepository.FindUserById(userId);
        return _mapper.Map<UserDTO>(user);
    }
}
