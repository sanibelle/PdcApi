using Microsoft.AspNetCore.Identity;
using Pdc.Domain.Models.Security;
using System.Net.Http.Json;
using TestDataSeeder;

namespace Pdc.E2ETests;

[TestFixture]
public class UserApiTests : ApiTestBase
{
    private IdentityRole<Guid> _competencyRole = null!;
    private IdentityRole<Guid> _userRole = null!;
    private IdentityRole<Guid> _adminRole = null!;

    [OneTimeSetUp]
    public void SetUpUserTests()
    {
        _competencyRole = DataSeeder.ExistingRoles.Find(x => x.Name == Roles.Competency)
            ?? throw new InvalidOperationException($"Role '{Roles.Competency}' not found in existing roles.");
        _adminRole = DataSeeder.ExistingRoles.Find(x => x.Name == Roles.Admin)
            ?? throw new InvalidOperationException($"Role '{Roles.Admin}' not found in existing roles.");
        _userRole = DataSeeder.ExistingRoles.Find(x => x.Name == Roles.User)
            ?? throw new InvalidOperationException($"Role '{Roles.User}' not found in existing roles.");
    }

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
        // Arrange - Get the user and ensure they do not have the competency role
        var response = await _Client.GetAsync($"/api/user");
        var users = await response.Content.ReadFromJsonAsync<List<User>>();
        var user = users.FirstOrDefault(x => x.Id == DataSeeder.UserForRoleTest.Id);
        Assert.That(user, Is.Not.Null);
        Assert.That(user.Roles.Any(x => x == _competencyRole.Name), Is.False);

        // Act - Add the role to the user
        var roles = user.Roles.ToList();
        roles.Add(_competencyRole.Name!);

        response = await _Client.PutAsJsonAsync($"/api/user/{DataSeeder.UserForRoleTest.Id}/roles", roles);

        // Assert - User should now have the competency role
        user = await response.Content.ReadFromJsonAsync<User>();
        Assert.That(user, Is.Not.Null);
        Assert.That(user.Roles.Any(x => x == _competencyRole.Name), Is.True);

        // Act - Remove the role
        roles.Remove(_competencyRole.Name!);
        response = await _Client.PutAsJsonAsync($"/api/user/{DataSeeder.UserForRoleTest.Id}/roles", roles);

        // Assert - User should no longer have the competency role
        user = await response.Content.ReadFromJsonAsync<User>();
        Assert.That(user, Is.Not.Null);
        Assert.That(user.Roles.Any(x => x == _competencyRole.Name), Is.False);
    }

    [Test]
    public async Task GivenUserNotAdminWithOtherRoles_WhenSettingAdmin_ThenShouldBecomeAdminAndKeepRoles()
    {
        // Arrange - Get the user by id
        var response = await _Client.GetAsync($"/api/user");
        var users = await response.Content.ReadFromJsonAsync<List<User>>();
        var user = users.FirstOrDefault(x => x.Id == DataSeeder.UserForRoleTest.Id);
        Assert.That(user, Is.Not.Null);
        Assert.That(user.Id, Is.EqualTo(DataSeeder.UserForRoleTest.Id));
        Assert.That(user.Roles.Any(x => x == _userRole.Name), Is.False);

        // Act - Add user role to the user
        var roles = user.Roles.ToList();
        roles.Add(_userRole.Name!);

        response = await _Client.PutAsJsonAsync($"/api/user/{DataSeeder.UserForRoleTest.Id}/roles", roles);
        user = await response.Content.ReadFromJsonAsync<User>();
        Assert.That(user, Is.Not.Null);
        Assert.That(user.Roles.Any(x => x == _userRole.Name), Is.True);

        // Act - Add admin role to the user
        roles.Add(_adminRole.Name);
        response = await _Client.PutAsJsonAsync($"/api/user/{DataSeeder.UserForRoleTest.Id}/roles", roles);
        user = await response.Content.ReadFromJsonAsync<User>();

        // Assert - User should have both roles
        Assert.That(user, Is.Not.Null);
        Assert.That(user.Roles.Any(x => x == _userRole.Name), Is.True);
        Assert.That(user.Roles.Any(x => x == _adminRole.Name), Is.True);
    }

    [Test]
    public async Task GivenCurrentAdmin_WhenRemovingSelfAdminRole_ThenShouldHaveError()
    {
        // Arrange - No roles in the array
        string[] roles = [];

        // Act - remove all the roles from the admin requesting to remove the roles.
        var response = await _Client.PutAsJsonAsync($"/api/user/{DataSeeder.Admin.Id}/roles", roles);
        Assert.That(response.StatusCode == System.Net.HttpStatusCode.Forbidden);
    }
}

