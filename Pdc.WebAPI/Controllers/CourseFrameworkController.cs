using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pdc.Application.DTOS;
using Pdc.Application.DTOS.CourseFramework;
using Pdc.Domain.Interfaces.UseCases.CourseFramework;
using Pdc.Domain.Models.Security;

namespace Pdc.WebAPI.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class CourseFrameworkController(
                                IGetCourseFrameworkByCodeUseCase getCourseFrameworkByCodeUseCase,
                                IDeleteCourseFrameworkUseCase deleteCourseFrameworkUseCase
    //UserControllerService userControllerService
    ) : ControllerBase
{

    [Authorize(Roles = Roles.CourseFramework)]
    [HttpGet("{courseFrameworkCode}")]
    public async Task<ActionResult<UntrackedCourseFrameworkDTO>> GetCourseFrameworkByCode(string courseFrameworkCode)
    {
        UntrackedCourseFrameworkDTO courseFramework = await getCourseFrameworkByCodeUseCase.Execute(courseFrameworkCode);
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
    [HttpPut("{programOfStudyCode}/courseFramework/{courseFrameworkCode}")]
    public async Task<ActionResult<CompetencyDTO>> UpdateCourseFramework(string programOfStudyCode, string courseFrameworkCode, [FromBody] CompetencyDTO updateCompetencyDTO)
    {
        throw new NotImplementedException();

        //User user = userControllerService.GetUserFromHttpContext();
        //if (updateCompetencyDTO.ChangeRecordNumber == 1 && updateCompetencyDTO.IsDraft)
        //{
        //    CompetencyDTO competency = await updateDraftV1CompetencyUseCase.Execute(programOfStudyCode, competencyCode, updateCompetencyDTO, user);
        //    return Ok(competency);
        //}
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
