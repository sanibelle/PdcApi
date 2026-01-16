using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pdc.Infrastructure.Entities.Identity;

namespace TestDataSeeder.SeedData;

internal class User
{
    private readonly UserManager<IdentityUserEntity> _userManager;
    public User(UserManager<IdentityUserEntity> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IdentityUserEntity> SeedAsync(string? role = null)
    {
        var user = new IdentityUserEntity
        {
            UserName = $"Test{role}",
            Email = $"test{role}@test.com",
            EmailConfirmed = true
        };

        IdentityResult result = await _userManager.CreateAsync(user);
        if (!string.IsNullOrEmpty(role))
        {
            var roleResult = await _userManager.AddToRoleAsync(user, role);
            if (!result.Succeeded || !roleResult.Succeeded)
            {
                throw new Exception($"Failed to create user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
        }
        return await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == user.UserName);
    }
}
