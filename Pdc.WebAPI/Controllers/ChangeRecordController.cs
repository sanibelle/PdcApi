using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pdc.Domain.DTOS.Common;
using Pdc.Domain.Interfaces.UseCases.Versioning;
using Pdc.Domain.Models.Security;

namespace Pdc.WebAPI.Controllers;

[ApiController]
[Authorize(Roles = Roles.PublishChangeRecord)]
[Route("api/[controller]")]
public class ChangeRecordController(
                        IPublishChangeRecordUseCase publishChangeRecordUseCase) : ControllerBase
{
    [HttpPost("publish/{changeRecordId}")]
    public async Task<ActionResult<ComplementaryInformationDTO>> Publish(Guid changeRecordId)
    {
        ChangeRecordDTO changeRecord = await publishChangeRecordUseCase.Execute(changeRecordId);
        return Ok(changeRecord);
    }
}
