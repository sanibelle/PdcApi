namespace Pdc.Domain.DTOS.Common;

public class UserDTO
{
    public string Id { get; set; } = "";
    public string UserName { get; set; } = "";
    public List<string> Roles { get; set; } = [];
}
