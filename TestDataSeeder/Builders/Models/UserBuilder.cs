using Pdc.Domain.Models.Security;

namespace TestDataSeeder.Builders.Models;

public class UserBuilder
{
    private Guid _id = Guid.NewGuid();
    private string _displayName = $"TestUser{new DateTime()}";
    private string _email = $"Email{new DateTime()}";
    private string[] _roles = [];

    public UserBuilder() { }

    public UserBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public UserBuilder WithDisplayName(string displayName)
    {
        _displayName = displayName;
        return this;
    }

    public UserBuilder WithEmail(string email)
    {
        _email = email;
        return this;
    }

    public UserBuilder WithRoles(string[] roles)
    {
        _roles = roles;
        return this;
    }

    public User Build()
    {
        return new User
        {
            Id = _id,
            UserName = _displayName,
            Email = _email,
            Roles = _roles
        };
    }
}
