using Microsoft.AspNetCore.Identity;
using Pdc.Domain.Models.Security;
using Pdc.Infrastructure.Identity;
using System.Net.Http.Json;
using TestDataSeeder;
using TestDataSeeder.Builders.DTOS;

namespace Pdc.E2ETests;

[TestFixture]
public class UserApiTests : ApiTestBase
{

    [Test]
    public async Task GivenGetAllUsersRequest_ThenShouldReturnTheUsersWithTheirRoles()
    {
        // Arrange
        var response = await _Client.GetAsync($"/api/user");
        var users = await response.Content.ReadFromJsonAsync<List<User>>();
        Assert.That(users, Is.Not.Empty);
        var user = users.First();
        Assert.That(user, Is.Not.Null);


    }

    [Test]
    public async Task GivenUserWithMissingRoles_WhenUpdatingRoles_ThenShouldHaveRoles()
    {
        // Arrange
        IdentityRole<Guid>? competencyRole = DataSeeder.ExistingRoles.Find(x => x.Name == Roles.Competency);
        if (competencyRole == null)
        {
            Assert.Fail("Role 'Competency' not found in existing roles.");
            return;
        }

        // Get the user and ensure they do not have the competency role
        var response = await _Client.GetAsync($"/api/user");
        var users = await response.Content.ReadFromJsonAsync<List<User>>();
        var user = users.FirstOrDefault(x => x.Id== DataSeeder.UserForRoleTest.Id);
        Assert.That(user, Is.Not.Null);
        Assert.That(user.Roles.Any(x => x == competencyRole.Name), Is.False);


        // Creating the DTO and adding the role to the user
        var roles = user.Roles.ToList();
        roles.Add(competencyRole.Name);
        
        response = await _Client.PutAsJsonAsync($"/api/user/{DataSeeder.UserForRoleTest.Id}/roles", roles);
        user = await response.Content.ReadFromJsonAsync<User>();
        Assert.That(user, Is.Not.Null);
        Assert.That(user.Roles.Any(x => x == competencyRole.Name), Is.True);

        // Now let's remove the role.
        roles.Remove(competencyRole.Name);
        response = await _Client.PutAsJsonAsync($"/api/user/{DataSeeder.UserForRoleTest.Id}/roles", roles);
        user = await response.Content.ReadFromJsonAsync<User>();
        Assert.That(user, Is.Not.Null);
        Assert.That(user.Roles.Any(x => x == competencyRole.Name), Is.False);
    }

    [Test]
    public async Task GivenUserNotAdminWithOtherRoles_WhenSettingAdmin_ThenShouldBecomeAdminAndKeepRoles()
    {
    }

    [Test]
    public async Task GivenCurrentAdmin_WhenRemovingSelfAdminRole_ThenShouldHaveError()
    {
    }
}

