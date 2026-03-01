using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pdc.Domain.DTOS.Common;
using Pdc.Domain.Interfaces.UseCases.Version;
using Pdc.Domain.Models.Security;
using Pdc.WebAPI.Services;

namespace Pdc.WebAPI.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class ComplementaryInformation(
                        IDeleteComplementaryInformationUseCase deleteComplementartInformationUseCase,
                        IUpdateComplementaryInformationUseCase updateComplementartInformationUseCase,
                        IGetComplementaryInformationUseCase getComplementartInformationUseCase,
                        UserControllerService userControllerService) : ControllerBase
{
    private readonly IDeleteComplementaryInformationUseCase _deleteComplementartInformationUseCase = deleteComplementartInformationUseCase;
    private readonly IUpdateComplementaryInformationUseCase _updateComplementartInformationUseCase = updateComplementartInformationUseCase;
    private readonly IGetComplementaryInformationUseCase _getComplementartInformationUseCase = getComplementartInformationUseCase;
    private readonly UserControllerService _userControllerService = userControllerService;


    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<ComplementaryInformationDTO>> Get(Guid id)
    {
        return await _getComplementartInformationUseCase.Execute(id);
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ComplementaryInformationDTO>> Delete(Guid id)
    {
        User user = _userControllerService.GetUserFromHttpContext();
        await _deleteComplementartInformationUseCase.Execute(id, user);
        return NoContent();
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult<ComplementaryInformationDTO>> Update(Guid id, [FromBody] ComplementaryInformationDTO complementaryInformationDTO)
    {
        User user = _userControllerService.GetUserFromHttpContext();
        ComplementaryInformationDTO updatedComplementaryInformation = await _updateComplementartInformationUseCase.Execute(complementaryInformationDTO, user, id);
        return Ok(updatedComplementaryInformation);
    }

}
