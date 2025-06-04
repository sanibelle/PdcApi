using Pdc.Application.DTOS.Common;

namespace Pdc.Tests.Builders.DTOS;

public class UserDTOBulder
{
    private string _displayName = $"TEstUser{Guid.NewGuid().ToString().Substring(0, 8)}";

    public UserDTOBulder WithDisplayName(string value)
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
