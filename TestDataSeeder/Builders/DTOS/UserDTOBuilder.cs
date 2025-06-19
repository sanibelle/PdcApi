using Pdc.Application.DTOS.Common;

namespace TestDataSeeder.Builders.DTOS;

public class UserDTOBuilder
{
    private string _displayName = $"TEstUser{Guid.NewGuid().ToString().Substring(0, 8)}";

    public UserDTOBuilder WithDisplayName(string value)
    {
        _displayName = value;
        return this;
    }

    public UserDTO Build()
    {
        return new UserDTO
        {
            DisplayName = _displayName
        };
    }

}
