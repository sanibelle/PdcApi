namespace Pdc.Domain.Models.Security;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? Email { get; set; }
    public string DisplayName { get; set; }

    public User()
    {
        Id = Guid.NewGuid();
        DisplayName = string.Empty;
    }
}
