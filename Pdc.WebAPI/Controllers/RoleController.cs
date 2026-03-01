using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pdc.Domain.Interfaces.UseCases.Role;
using Pdc.Domain.Models.Security;

namespace Pdc.WebAPI.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class RoleController(
                        IGetRolesUseCase getRolesUseCase) : ControllerBase
{
    private readonly IGetRolesUseCase _getRolesUseCase = getRolesUseCase;

    [Authorize(Roles = Roles.Admin)]
    [HttpGet]
    public async Task<ActionResult<IList<string>>> GetAll()
    {
        var roles = await _getRolesUseCase.Execute();
        return Ok(roles);
    }
}
