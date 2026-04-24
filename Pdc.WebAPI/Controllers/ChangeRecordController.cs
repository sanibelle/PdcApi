using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pdc.Domain.DTOS.Common;
using Pdc.Domain.Interfaces.UseCases.Versioning;
using Pdc.Domain.Models.Security;
using Pdc.WebAPI.Services;

namespace Pdc.WebAPI.Controllers;

[ApiController]
[Authorize(Roles = Roles.PublishChangeRecord)]
[Route("api/[controller]")]
public class ChangeRecordController(
                        IPublishChangeRecordUseCase publishChangeRecordUseCase,
                        UserControllerService userControllerService) : ControllerBase
{
    [HttpPost("publish/{changeRecordId}")]
    public async Task<ActionResult<ChangeRecordDTO>> Publish(Guid changeRecordId)
    {
        ChangeRecordDTO changeRecord = await publishChangeRecordUseCase.Execute(changeRecordId, userControllerService.GetUserFromHttpContext());
        return Ok(changeRecord);
    }
}
