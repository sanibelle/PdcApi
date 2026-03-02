namespace Pdc.Domain.Models.Security;

public class User
{
    public Guid? Id { get; set; }
    public string? Email { get; set; }
    public string UserName { get; set; }
    public IList<string> Roles { get; set; } = [];

    public User()
    {
        UserName = string.Empty;
    }

    public bool IsAdmin
    {
        get
        {
            return Roles.Contains(Security.Roles.Admin);
        }
    }
}
