using AutoMapper;
using Pdc.Application.DTOS.Common;
using Pdc.Domain.Exceptions;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Interfaces.UseCases.User;
using Pdc.Domain.Models.Security;
using Pdc.Infrastructure.Identity;

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

    public async Task<UserDTO> Execute(Guid userId, string[] targetRoles, User currentUser)
    {
        IList<string> currentRoles = await _userRepository.FindUserRolesByUserId(userId);
        
        // Check if user is trying to remove their own admin role
        if (currentUser.Id == userId && 
            currentRoles.Contains(Roles.Admin) && 
            !targetRoles.Contains(Roles.Admin))
        {
            throw new ForbiddenException("You cannot remove your own admin role.");
        }
        
        List<string> rolesToAdd = targetRoles.Where(x => !currentRoles.Any(r => r == x)).ToList();
        List<string> rolesToRemove = currentRoles.Where(x => !targetRoles.Any(r => r == x)).ToList();
        
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
