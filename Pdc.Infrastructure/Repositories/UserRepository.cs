using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Pdc.Domain.Exceptions;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Models.MinisterialSpecification;
using Pdc.Domain.Models.Security;
using Pdc.Infrastructure.Data;
using Pdc.Infrastructure.Entities.Identity;
using Pdc.Infrastructure.Exceptions;
using Pdc.Infrastructure.Identity;

namespace Pdc.Infrastructure.Repositories;

public class UserRepository(AppDbContext context, UserManager<IdentityUserEntity> userManager, IMapper mapper) : IUserRepository
{
    private readonly AppDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    private readonly UserManager<IdentityUserEntity> _userManager = userManager;

    public async Task<List<User>> GetUsersWithRoles()
    {

        List<User> users = (await _context.Users
            .Select(u => new
            {
                User = u,
                Roles = _context.UserRoles
                    .Where(ur => ur.UserId == u.Id)
                    .Join(_context.Roles,
                        ur => ur.RoleId,
                        r => r.Id,
                        (ur, r) => r)
                    .ToList()
            })
            .ToListAsync()).Select(x =>
        {
            User user = _mapper.Map<User>(x.User);
            user.Roles = x.Roles
                .Select(r => r.Name)
                .Where(x => x != null)
                .Cast<string>()
                .ToList();
            return user;
        }).ToList();

        return users;
    }

    public async Task<User> SetUserRoles(Guid userId, IList<string> rolesToAdd, IList<string> rolesToRemove)
    {
        IdentityUserEntity user = await FindUserEntityByIdAndThrowIfNull(userId);
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            foreach (var role in rolesToAdd)
            {
                IdentityResult addResult = await _userManager.AddToRoleAsync(user, role);
                if (!addResult.Succeeded)
                    throw new Exception("Failed to add role");
            }
            foreach (var role in rolesToRemove)
            {
                IdentityResult removeResult = await _userManager.RemoveFromRoleAsync(user, role);
                if (!removeResult.Succeeded)
                    throw new Exception("Failed to remove role");
            }
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
        return await FindUserById(userId);
    }

    public async Task<User> FindUserById(Guid userId)
    {
        var userEntity = await FindUserEntityByIdAndThrowIfNull(userId);
        var roles = await _userManager.GetRolesAsync(userEntity);
        User user = _mapper.Map<User>(userEntity);
        user.Roles = roles;
        return user;
    }

    public async Task<IList<string>> FindUserRolesByUserId(Guid userId)
    {
        var userEntity = await FindUserEntityByIdAndThrowIfNull(userId);
        return await _userManager.GetRolesAsync(userEntity);
    }

    private async Task<IdentityUserEntity> FindUserEntityByIdAndThrowIfNull(Guid userId)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.Id == userId);
        if (user == null)
            throw new NotFoundException(nameof(User), userId);
        return user;
    }

    public async Task<IList<string>> GetAllRoles()
    {
        return await _context.Roles
            .Select(r => r.Name)
            .Where(x => x != null)
            .Cast<string>()
            .ToListAsync();
    }

    public bool IsAdminRoleInArray(IList<string> roles)
    {
        return roles.Contains(Roles.Admin);
    }
}
