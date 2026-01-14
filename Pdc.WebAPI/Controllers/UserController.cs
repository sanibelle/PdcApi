using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pdc.Application.DTOS;
using Pdc.Application.DTOS.Common;
using Pdc.Application.UseCases;
using Pdc.Domain.Interfaces.UseCases.Competency;
using Pdc.Domain.Interfaces.UseCases.ProgramOfStudy;
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

    [Authorize(Roles = Roles.Access)]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDTO>>> GetAll()
    {
        var users = await _getUsersUseCase.Execute();
        return Ok(users);
    }

    [Authorize(Roles = Roles.Access)]
    [HttpGet("{userId}")]
    public async Task<ActionResult<UserDTO>> SetRoles(Guid userId)  
    {
        User currentUser = _userControllerService.GetUserFromHttpContext();
        return Ok(await _getUserUseCase.Execute(userId));
    }

    [Authorize(Roles = Roles.Access)]
    [HttpPut("{userId}/roles")]
    public async Task<ActionResult<UserDTO>> SetRoles(Guid userId, [FromBody] string[] roles)
    {
        User currentUser = _userControllerService.GetUserFromHttpContext();
        return Ok(await _setUserRolesUseCase.Execute(userId, roles, currentUser));
    }

}
