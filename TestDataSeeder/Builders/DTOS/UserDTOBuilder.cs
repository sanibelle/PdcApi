using Pdc.Application.DTOS.Common;

namespace TestDataSeeder.Builders.DTOS;

public class UserDTOBuilder
{
    private string _userName = $"TEstUser{Guid.NewGuid().ToString().Substring(0, 8)}";

    public UserDTOBuilder WithUserName(string value)
    {
        _userName = value;
        return this;
    }

    public UserDTO Build()
    {
        return new UserDTO
        {
            UserName = _userName
        };
    }

}
