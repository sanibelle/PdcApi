using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pdc.Domain.DTOS.Common;
using Pdc.Domain.Interfaces.UseCases.User;
using Pdc.Domain.Models.Security;
using Pdc.Infrastructure.Identity;
using Pdc.WebAPI.Services;

namespace Pdc.WebAPI.Controllers;
[ApiController]
[Authorize]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IGetUserUseCase _getUserUseCase;
    private readonly IGetUsersUseCase _getUsersUseCase;
    private readonly ISetUserRolesUseCase _setUserRolesUseCase;
    private readonly UserControllerService _userControllerService;

    public UserController(
                            IGetUserUseCase getUserUseCase,
                            IGetUsersUseCase getUsersUseCase,
                            ISetUserRolesUseCase setUserRolesUseCase,
                            UserControllerService userControllerService)
    {
        _getUserUseCase = getUserUseCase;
        _getUsersUseCase = getUsersUseCase;
        _setUserRolesUseCase = setUserRolesUseCase;
        _userControllerService = userControllerService;
    }

    [Authorize(Roles = Roles.User)]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDTO>>> GetAll()
    {
        var users = await _getUsersUseCase.Execute();
        return Ok(users);
    }

    // only the admin can set the roles of other users
    [Authorize(Roles = Roles.Admin)]
    [HttpGet("{userId}")]
    public async Task<ActionResult<UserDTO>> SetRoles(Guid userId)  
    {
        User currentUser = _userControllerService.GetUserFromHttpContext();
        return Ok(await _getUserUseCase.Execute(userId));
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpPut("{userId}/roles")]
    public async Task<ActionResult<UserDTO>> GetRoles(Guid userId, [FromBody] string[] roles)
    {
        User currentUser = _userControllerService.GetUserFromHttpContext();
        return Ok(await _setUserRolesUseCase.Execute(userId, roles, currentUser));
    }

}
