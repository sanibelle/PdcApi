using Pdc.Domain.Models.Security;
using System.Security.Claims;

namespace Pdc.WebAPI.Services;

public class UserControllerService
{
    public User GetUserFromHttpContext(IHttpContextAccessor httpContextAccessor)
    {
        ClaimsPrincipal? user = httpContextAccessor.HttpContext?.User;
        if (user == null || !user.Identity?.IsAuthenticated == true)
        {
            throw new UnauthorizedAccessException("Could not find the user in the context");
        }
        string? userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null)
        {
            throw new UnauthorizedAccessException("Could not find userId in the context");
        }

        return new User
        {
            Id = Guid.Parse(userId),
            // TODO Email = user.FindFirstValue(ClaimTypes.Email) ?? "",
            DisplayName = user.FindFirstValue(ClaimTypes.Name) ?? ""
        };
    }
}
