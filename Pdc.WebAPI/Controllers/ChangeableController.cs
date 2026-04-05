using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pdc.Domain.DTOS.Common;
using Pdc.Domain.Interfaces.UseCases.ChangeRecord;
using Pdc.Domain.Models.Security;
using Pdc.WebAPI.Services;

namespace Pdc.WebAPI.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class ChangeableController(
                        IAddComplementaryInformationUseCase addComplementaryInformationUseCase,
                        UserControllerService userControllerService) : ControllerBase
{
    private readonly IAddComplementaryInformationUseCase _addComplementaryInformationUseCase = addComplementaryInformationUseCase;
    private readonly UserControllerService _userControllerService = userControllerService;


    [HttpPost("{changeableId}/complementaryInformation")]
    public async Task<ActionResult<ComplementaryInformationDTO>> Add(Guid changeableId, [FromBody] ComplementaryInformationDTO complementaryInformationDTO)
    {
        User user = _userControllerService.GetUserFromHttpContext();
        ComplementaryInformationDTO createdComplementaryInformation = await _addComplementaryInformationUseCase.Execute(complementaryInformationDTO, changeableId, user);
        return CreatedAtAction(
            nameof(ComplementaryInformation.Get),
            controllerName: "ComplementaryInformation",
            new { id = createdComplementaryInformation.Id },
            createdComplementaryInformation);
    }
}
