using Microsoft.AspNetCore.Identity;
using Pdc.Infrastructure.Identity;

namespace TestDataSeeder.SeedData;

internal class Role : ISeeder<List<IdentityRole<Guid>>>
{
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;
    public Role(RoleManager<IdentityRole<Guid>> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<List<IdentityRole<Guid>>> SeedAsync()
    {
        var roles = new List<IdentityRole<Guid>>();

        foreach (var property in typeof(Roles).GetFields())
        {
            string? name = property.GetRawConstantValue()?.ToString();
            if (string.IsNullOrEmpty(name))
            {
                throw new Exception("Failed to get the constant value of a role");
            }

            var role = new IdentityRole<Guid>(name); 
            roles.Add(role);
            await _roleManager.CreateAsync(role);
        }
        return roles;
    }
}
