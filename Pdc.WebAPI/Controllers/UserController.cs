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
    private readonly IGetUsersUseCase _getUsersUseCase;
    private readonly ISetUserRolesUseCase _setUserRolesUseCase;
    private readonly UserControllerService _userControllerService;

    public UserController(
                            IGetUsersUseCase getUsersUseCase,
                            ISetUserRolesUseCase setUserRolesUseCase,
                            UserControllerService userControllerService)
    {
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
    [HttpPut("{userId}/roles")]
    public async Task<ActionResult<UserDTO>> SetRoles(Guid userId, [FromBody] string[] roles)
    {
        return Ok(await _setUserRolesUseCase.Execute(userId, roles));
    }

    //[Authorize(Roles = Roles.StudyProgram)]
    //[HttpPut("{code}")]
    //public async Task<IActionResult> Update(string code, [FromBody] ProgramOfStudyDTO programOfStudyDTO)
    //{
    //    var program = await _updateUseCase.Execute(code, programOfStudyDTO);
    //    return Ok(program);
    //}

    //[Authorize(Roles = Roles.StudyProgram)]
    //[HttpDelete("{code}")]
    //public async Task<IActionResult> Delete(string code)
    //{
    //    await _deleteProgramOfStudyUseCase.Execute(code);
    //    return NoContent();
    //}
}
