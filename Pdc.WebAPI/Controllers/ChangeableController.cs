using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pdc.Domain.DTOS.Common;
using Pdc.Domain.Interfaces.UseCases.Version;

namespace Pdc.WebAPI.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class ChangeableController(
                        IAddComplementaryInformationUseCase addComplementartInformationUseCase,
                        IDeleteComplementaryInformationUseCase deleteComplementartInformationUseCase,
                        IUpdateComplementaryInformationUseCase updateComplementartInformationUseCase) : ControllerBase
{
    private readonly IAddComplementaryInformationUseCase _addComplementartInformationUseCase = addComplementartInformationUseCase;
    private readonly IDeleteComplementaryInformationUseCase _deleteComplementartInformationUseCase = deleteComplementartInformationUseCase;
    private readonly IUpdateComplementaryInformationUseCase _updateComplementartInformationUseCase = updateComplementartInformationUseCase;

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<ComplementaryInformationDTO>> Add(ComplementaryInformationDTO complementaryInformation)
    {
        return Ok();
    }
}
