using AutoMapper;
using Pdc.Domain.DTOS.Common;
using Pdc.Domain.Exceptions;
using Pdc.Domain.Interfaces.Repositories;
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

    public async Task<UserDTO> Execute(Guid userId, string[] targetRoles, User currentUser)
    {
        await IsTargetRolesExists(targetRoles);
        IList<string> currentRoles = await GetUserRoles(userId);
        PreventAdminFromDemotingSelf(userId, targetRoles, currentUser, currentRoles);

        List<string> rolesToAdd = targetRoles.Except(currentRoles).ToList();
        List<string> rolesToRemove = currentRoles.Except(targetRoles).ToList();

        User user = await _userRepository.SetUserRoles(userId, rolesToAdd, rolesToRemove);
        return _mapper.Map<UserDTO>(user);
    }

    private async void PreventAdminFromDemotingSelf(Guid userId, IList<string> targetRoles, User currentUser, IList<string> currentRoles)
    {
        ;
        if (currentUser.Id == userId &&
                    _userRepository.IsAdminRoleInArray(currentRoles) &&
                    !_userRepository.IsAdminRoleInArray(targetRoles))
        {
            throw new ForbiddenException("You cannot remove your own admin role.");
        }
    }

    private async Task<IList<string>> GetUserRoles(Guid userId)
    {
        return await _userRepository.FindUserRolesByUserId(userId);
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
