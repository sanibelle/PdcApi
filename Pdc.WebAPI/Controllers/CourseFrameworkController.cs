using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pdc.Application.DTOS;
using Pdc.Application.DTOS.CourseFramework;
using Pdc.Domain.Interfaces.UseCases.CourseFramework;
using Pdc.Domain.Models.Security;
using Pdc.WebAPI.Services;

namespace Pdc.WebAPI.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class CourseFrameworkController(
        IGetCourseFrameworkByIdUseCase getCourseFrameworkByCodeUseCase,
        IDeleteCourseFrameworkUseCase deleteCourseFrameworkUseCase,
        IUpdateDraftV1CourseFramekworkUseCase updateDraftV1CourseFramekworkUseCase,
        UserControllerService userControllerService
    ) : ControllerBase
{

    [Authorize(Roles = Roles.CourseFramework)]
    [HttpGet("{id}")]
    public async Task<ActionResult<TrackedCourseFrameworkDTO>> GetCourseFrameworkByCode(Guid id)
    {
        TrackedCourseFrameworkDTO courseFramework = await getCourseFrameworkByCodeUseCase.Execute(id);
        return Ok(courseFramework);
    }


    [Authorize(Roles = Roles.CourseFramework)]
    [HttpGet("{programOfStudyCode}/courseFramework/{courseFrameworkCode}/v{versionNumber}")]
    public async Task<ActionResult<CompetencyDTO>> GetCourseFramework(string programOfStudyCode, string courseFrameworkCode, int versionNumber)
    {
        throw new NotImplementedException();

        //CompetencyDTO competency = await getCompetencyWithChangeDetailsUseCase.Execute(programOfStudyCode, competencyCode, versionNumber);
        //return Ok(competency);
    }

    [Authorize(Roles = Roles.CourseFramework)]
    [HttpPut("{courseFrameworkId}")]
    public async Task<ActionResult<CompetencyDTO>> UpdateCourseFramework(Guid courseFrameworkId, [FromBody] TrackedCourseFrameworkDTO updateCourseFrameworkDTO)
    {
        User user = userControllerService.GetUserFromHttpContext();
        if (updateCourseFrameworkDTO.ChangeRecordNumber == 1 && updateCourseFrameworkDTO.IsDraft)
        {
            TrackedCourseFrameworkDTO courseFramework = await updateDraftV1CourseFramekworkUseCase.Execute(courseFrameworkId, updateCourseFrameworkDTO, user);
            return Ok(courseFramework);
        }
        return Problem("Not done yet");
        //else
        //{
        //    CompetencyDTO competency = await updatePublishedCompetencyUseCase.Execute(programOfStudyCode, competencyCode, updateCompetencyDTO, user);
        //    return Ok(competency);
        //}
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpDelete("{courseFrameworkId}")]
    public async Task<ActionResult> DeleteCourseFramework(Guid courseFrameworkId)
    {
        await deleteCourseFrameworkUseCase.Execute(courseFrameworkId);
        return NoContent();
    }
}
