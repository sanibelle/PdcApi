using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pdc.Domain.DTOS.Common;
using Pdc.Domain.Interfaces.UseCases.Versioning;
using Pdc.Domain.Models.Security;
using Pdc.WebAPI.Services;

namespace Pdc.WebAPI.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class ComplementaryInformationController(
                        IDeleteComplementaryInformationUseCase deleteComplementaryInformationUseCase,
                        IUpdateComplementaryInformationUseCase updateComplementaryInformationUseCase,
                        IGetComplementaryInformationUseCase getComplementaryInformationUseCase,
                        UserControllerService userControllerService) : ControllerBase
{
    private readonly IDeleteComplementaryInformationUseCase _deleteComplementaryInformationUseCase = deleteComplementaryInformationUseCase;
    private readonly IUpdateComplementaryInformationUseCase _updateComplementaryInformationUseCase = updateComplementaryInformationUseCase;
    private readonly IGetComplementaryInformationUseCase _getComplementaryInformationUseCase = getComplementaryInformationUseCase;
    private readonly UserControllerService _userControllerService = userControllerService;


    [HttpGet("{id}")]
    public async Task<ActionResult<ComplementaryInformationDTO>> Get(Guid id)
    {
        return Ok(await _getComplementaryInformationUseCase.Execute(id));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        User user = _userControllerService.GetUserFromHttpContext();
        await _deleteComplementaryInformationUseCase.Execute(id, user);
        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ComplementaryInformationDTO>> Update(Guid id, [FromBody] ComplementaryInformationDTO complementaryInformationDTO)
    {
        User user = _userControllerService.GetUserFromHttpContext();
        ComplementaryInformationDTO updatedComplementaryInformation = await _updateComplementaryInformationUseCase.Execute(complementaryInformationDTO, user, id);
        return Ok(updatedComplementaryInformation);
    }

}
