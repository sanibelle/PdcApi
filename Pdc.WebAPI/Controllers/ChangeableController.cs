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
public class ChangeableController(
                        IAddComplementaryInformationUseCase addComplementaryInformationUseCase,
                        IUpdateChangeableUseCase updateChangeableUseCase,
                        UserControllerService userControllerService) : ControllerBase
{
    [HttpPost("{changeableId}/complementaryInformation")]
    public async Task<ActionResult<ComplementaryInformationDTO>> Add(Guid changeableId, [FromBody] ComplementaryInformationDTO complementaryInformationDTO)
    {
        User user = userControllerService.GetUserFromHttpContext();
        ComplementaryInformationDTO createdComplementaryInformation = await addComplementaryInformationUseCase.Execute(complementaryInformationDTO, changeableId, user);
        return CreatedAtAction(
            nameof(ComplementaryInformationController.Get),
            controllerName: "ComplementaryInformation",
            new { id = createdComplementaryInformation.Id },
            createdComplementaryInformation);
    }

    [HttpPut("{changeableId}")]
    public async Task<ActionResult<ChangeableDTO>> Update(Guid changeableId, [FromBody] ChangeableDTO changeableDTO)
    {
        ChangeableDTO updatedChangeable = await updateChangeableUseCase.Execute(changeableDTO, changeableId);
        return Ok(updatedChangeable);
    }
}
