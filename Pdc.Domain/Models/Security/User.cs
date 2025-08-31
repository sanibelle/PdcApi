namespace Pdc.Domain.Models.Security;

public class User
{
    public Guid? Id { get; set; }
    public string? Email { get; set; }
    public string DisplayName { get; set; }

    public User()
    {
        DisplayName = string.Empty;
    }
}
