using Pdc.Infrastructure.Entities.Identity;

namespace Pdc.Tests.Builders.Entities;

public class IdentityUserEntityBuilder
{
    private Guid _id = Guid.NewGuid();
    private string _userName = $"user_{Guid.NewGuid().ToString().Substring(0, 8)}";
    private string _normalizedUserName = null;
    private string _email = $"user_{Guid.NewGuid().ToString().Substring(0, 8)}@example.com";
    private string _normalizedEmail = null;
    private bool _emailConfirmed = false;
    private string _passwordHash = Guid.NewGuid().ToString();
    private string _securityStamp = Guid.NewGuid().ToString();
    private string _concurrencyStamp = Guid.NewGuid().ToString();
    private string _phoneNumber = $"+1{new Random().Next(1000000000, 1999999999)}";
    private bool _phoneNumberConfirmed = false;
    private bool _twoFactorEnabled = false;
    private DateTimeOffset? _lockoutEnd = null;
    private bool _lockoutEnabled = false;
    private int _accessFailedCount = 0;

    public IdentityUserEntityBuilder() { }

    public IdentityUserEntityBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public IdentityUserEntityBuilder WithUserName(string userName)
    {
        _userName = userName;
        return this;
    }

    public IdentityUserEntityBuilder WithNormalizedUserName(string normalizedUserName)
    {
        _normalizedUserName = normalizedUserName;
        return this;
    }

    public IdentityUserEntityBuilder WithEmail(string email)
    {
        _email = email;
        return this;
    }

    public IdentityUserEntityBuilder WithNormalizedEmail(string normalizedEmail)
    {
        _normalizedEmail = normalizedEmail;
        return this;
    }

    public IdentityUserEntityBuilder WithEmailConfirmed(bool emailConfirmed)
    {
        _emailConfirmed = emailConfirmed;
        return this;
    }

    public IdentityUserEntityBuilder WithPasswordHash(string passwordHash)
    {
        _passwordHash = passwordHash;
        return this;
    }

    public IdentityUserEntityBuilder WithSecurityStamp(string securityStamp)
    {
        _securityStamp = securityStamp;
        return this;
    }

    public IdentityUserEntityBuilder WithConcurrencyStamp(string concurrencyStamp)
    {
        _concurrencyStamp = concurrencyStamp;
        return this;
    }

    public IdentityUserEntityBuilder WithPhoneNumber(string phoneNumber)
    {
        _phoneNumber = phoneNumber;
        return this;
    }

    public IdentityUserEntityBuilder WithPhoneNumberConfirmed(bool phoneNumberConfirmed)
    {
        _phoneNumberConfirmed = phoneNumberConfirmed;
        return this;
    }

    public IdentityUserEntityBuilder WithTwoFactorEnabled(bool twoFactorEnabled)
    {
        _twoFactorEnabled = twoFactorEnabled;
        return this;
    }

    public IdentityUserEntityBuilder WithLockoutEnd(DateTimeOffset? lockoutEnd)
    {
        _lockoutEnd = lockoutEnd;
        return this;
    }

    public IdentityUserEntityBuilder WithLockoutEnabled(bool lockoutEnabled)
    {
        _lockoutEnabled = lockoutEnabled;
        return this;
    }

    public IdentityUserEntityBuilder WithAccessFailedCount(int accessFailedCount)
    {
        _accessFailedCount = accessFailedCount;
        return this;
    }

    public IdentityUserEntity Build()
    {
        return new IdentityUserEntity
        {
            Id = _id,
            UserName = _userName,
            NormalizedUserName = _normalizedUserName ?? _userName?.ToUpper(),
            Email = _email,
            NormalizedEmail = _normalizedEmail ?? _email?.ToUpper(),
            EmailConfirmed = _emailConfirmed,
            PasswordHash = _passwordHash,
            SecurityStamp = _securityStamp,
            ConcurrencyStamp = _concurrencyStamp,
            PhoneNumber = _phoneNumber,
            PhoneNumberConfirmed = _phoneNumberConfirmed,
            TwoFactorEnabled = _twoFactorEnabled,
            LockoutEnd = _lockoutEnd,
            LockoutEnabled = _lockoutEnabled,
            AccessFailedCount = _accessFailedCount
        };
    }
}

/// WIP builder d'entites (pas fini de les ajouter, ne pas oublier le name space)