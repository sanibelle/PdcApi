using Microsoft.AspNetCore.Identity;
using Pdc.Infrastructure.Identity;

namespace TestDataSeeder.SeedData;

internal class Role : ISeeder<List<IdentityRole<Guid>>>
{
    private RoleManager<IdentityRole<Guid>> _roleManager;
    public Role(RoleManager<IdentityRole<Guid>> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<List<IdentityRole<Guid>>> SeedAsync()
    {
        var roles = new List<IdentityRole<Guid>>(); // Declare and initialize the 'roles' variable  

        foreach (var property in typeof(Roles).GetFields())
        {
            string? name = property.GetRawConstantValue()?.ToString();
            if (string.IsNullOrEmpty(name))
            {
                throw new Exception("Failed to get the constant value of a role");
            }

            var role = new IdentityRole<Guid>(name); // Create a new role  
            roles.Add(role); // Add the role to the list  
            await _roleManager.CreateAsync(role); // Create the role using the role manager  
        }
        return roles; // Return the list of roles  
    }
}
