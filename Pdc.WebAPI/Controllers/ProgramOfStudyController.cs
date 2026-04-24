using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pdc.Application.DTOS;
using Pdc.Domain.Interfaces.UseCases.Competency;
using Pdc.Domain.Interfaces.UseCases.ProgramOfStudy;
using Pdc.Domain.Models.Security;
using Pdc.WebAPI.Services;

namespace Pdc.WebAPI.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class ProgramOfStudyController(IAddProgramOfStudyUseCase createUseCase,
                                IDeleteProgramOfStudyUseCase deleteProgramOfStudyUseCase,
                                IGetProgramOfStudyUseCase getProgramOfStudyUseCase,
                                IGetProgramOfStudiesUseCase getProgramOfStudiesUseCase,
                                IUpdateProgramOfStudyUseCase updateUseCase,
                                IAddCompetencyUseCase createCompetencyUseCase,
                                IDeleteCompetencyUseCase deleteCompetencyUseCase,
                                IUpdateDraftV1CompetencyUseCase updateDraftV1CompetencyUseCase,
                                IUpdatePublishedCompetencyUseCase updatePublishedCompetencyUseCase,
                                IGetCompetenciesByProgramOfStudyUseCase getCompetenciesByProgramOfStudyUseCase,
                                IGetCompetencyUseCase getCompetencyUseCase,
                                UserControllerService userControllerService) : ControllerBase
{
    #region ProgramOfStudy
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProgramOfStudyDTO>>> GetAll()
    {
        var programOfStudies = await getProgramOfStudiesUseCase.Execute();
        return Ok(programOfStudies);
    }

    [Authorize(Roles = Roles.StudyProgram)]
    [HttpPost]
    public async Task<ActionResult<ProgramOfStudyDTO>> Create(ProgramOfStudyDTO programOfStudy)
    {
        return CreatedAtAction(
            nameof(Get),
            new { code = programOfStudy.Code },
            await createUseCase.Execute(programOfStudy));
    }

    [Authorize]
    [HttpGet("{code}")]
    public async Task<IActionResult> Get(string code)
    {
        var program = await getProgramOfStudyUseCase.Execute(code);
        return Ok(program);
    }

    [Authorize(Roles = Roles.StudyProgram)]
    [HttpPut("{code}")]
    public async Task<IActionResult> Update(string code, [FromBody] ProgramOfStudyDTO programOfStudyDTO)
    {
        var program = await updateUseCase.Execute(code, programOfStudyDTO);
        return Ok(program);
    }

    [Authorize(Roles = Roles.StudyProgram)]
    [HttpDelete("{code}")]
    public async Task<IActionResult> Delete(string code)
    {
        await deleteProgramOfStudyUseCase.Execute(code);
        return NoContent();
    }
    #endregion
    #region Competency
    [Authorize(Roles = Roles.Competency)]
    [HttpPost("{programOfStudyCode}/competency")]
    public async Task<ActionResult<CompetencyDTO>> AddCompetency(string programOfStudyCode, [FromBody] CompetencyDTO createCompetencyDTO)
    {
        User user = userControllerService.GetUserFromHttpContext();
        CompetencyDTO competency = await createCompetencyUseCase.Execute(programOfStudyCode, createCompetencyDTO, user);

        return CreatedAtAction(
            nameof(GetCompetency),
            new { programOfStudyCode, competencyCode = competency.Code },
            competency);
    }

    [Authorize(Roles = Roles.Competency)]
    [HttpGet("{programOfStudyCode}/competency/{competencyCode}")]
    public async Task<ActionResult<CompetencyDTO>> GetCompetency(string programOfStudyCode, string competencyCode)
    {
        CompetencyDTO competency = await getCompetencyUseCase.Execute(programOfStudyCode, competencyCode);
        return Ok(competency);
    }

    [Authorize(Roles = Roles.Competency)]
    [HttpGet("{programOfStudyCode}/competency")]
    public async Task<ActionResult<IList<CompetencyDTO>>> GetCompetencies(string programOfStudyCode)
    {
        IList<CompetencyDTO> competencies = await getCompetenciesByProgramOfStudyUseCase.Execute(programOfStudyCode);
        return Ok(competencies);
    }

    [Authorize(Roles = Roles.Competency)]
    [HttpPut("{programOfStudyCode}/competency/{competencyCode}")]
    public async Task<ActionResult<CompetencyDTO>> UpdateCompetency(string programOfStudyCode, string competencyCode, [FromBody] CompetencyDTO updateCompetencyDTO)
    {
        User user = userControllerService.GetUserFromHttpContext();
        if (updateCompetencyDTO.ChangeRecordNumber == 1 && updateCompetencyDTO.IsDraft)
        {
            CompetencyDTO competency = await updateDraftV1CompetencyUseCase.Execute(programOfStudyCode, competencyCode, updateCompetencyDTO, user);
            return Ok(competency);
        }
        else if (!updateCompetencyDTO.IsDraft)
        {
            CompetencyDTO competency = await updatePublishedCompetencyUseCase.Execute(programOfStudyCode, competencyCode, updateCompetencyDTO, user);
            return Ok(competency);
        }
        else
        {
            //TODO la mise à jour d'une version draft > 1.
            return BadRequest("Invalid update request for competency. Please check the ChangeRecordNumber and IsDraft properties.");
        }
    }

    [Authorize(Roles = Roles.Competency)]
    [HttpDelete("{programOfStudyCode}/competency/{competencyCode}")]
    public async Task<ActionResult> DeleteCompetency(string programOfStudyCode, string competencyCode)
    {
        await deleteCompetencyUseCase.Execute(programOfStudyCode, competencyCode);
        return NoContent();
    }
    #endregion
}
