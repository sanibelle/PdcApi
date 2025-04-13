using Pdc.Application.DTOS.Common;

namespace Pdc.Tests.Builders.DTOS;

public class UserDTOBulder
{
    private Guid _id = Guid.NewGuid();
    private string _displayName = $"TEstUser{Guid.NewGuid().ToString().Substring(0, 8)}";

    public UserDTOBulder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public UserDTOBulder WithDisplayName(string value)
    {
        _displayName = value;
        return this;
    }

    public UserDTO Build()
    {
        return new UserDTO
        {
            Id = _id,
            DisplayName = _displayName
        };
    }

}
