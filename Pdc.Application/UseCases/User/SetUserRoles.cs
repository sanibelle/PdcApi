using AutoMapper;
using Pdc.Application.DTOS.Common;
using Pdc.Domain.Exceptions;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Models.Security;
using Pdc.Infrastructure.Exceptions;
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
        await IsTargetRolesExists(targetRoles);
        IList<string> currentRoles = await GetUserRoles(userId);
        PreventAdminFromDemotingSelf(userId, targetRoles, currentUser, currentRoles);

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

    private void PreventAdminFromDemotingSelf(Guid userId, string[] targetRoles, User currentUser, IList<string> currentRoles)
    {
        if (currentUser.Id == userId &&
                    currentRoles.Contains(Roles.Admin) &&
                    !targetRoles.Contains(Roles.Admin))
        {
            throw new ForbiddenException("You cannot remove your own admin role.");
        }
    }

    private async Task<IList<string>> GetUserRoles(Guid userId)
    {
        try
        {
            return await _userRepository.FindUserRolesByUserId(userId);
        }
        catch (EntityNotFoundException ex)
        {
            throw new NotFoundException(ex.Message);
        }

    }

    private async Task IsTargetRolesExists(string[] targetRoles)
    {
        IList<string> existingRoles = await _userRepository.GetAllRoles();
        // checks if targetRoles exists
        var invalidRoles = targetRoles.Where(tr =>
            !existingRoles.Any(er => string.Equals(tr, er, StringComparison.OrdinalIgnoreCase)))
            .ToList();

        if (invalidRoles.Any())
        {
            throw new NotFoundException($"The following roles were not found: {string.Join(", ", invalidRoles)}");
        }
    }
}
