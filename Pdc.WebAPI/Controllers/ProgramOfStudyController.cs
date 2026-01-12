using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pdc.Application.DTOS;
using Pdc.Application.UseCases;
using Pdc.Domain.Interfaces.UseCases.Competency;
using Pdc.Domain.Interfaces.UseCases.ProgramOfStudy;
using Pdc.Domain.Models.Security;
using Pdc.Infrastructure.Identity;
using Pdc.WebAPI.Services;

namespace Pdc.WebAPI.Controllers;
[ApiController]
[Authorize]
[Route("api/[controller]")]
public class ProgramOfStudyController : ControllerBase
{
    private readonly ICreateProgramOfStudyUseCase _createUseCase;
    private readonly ICreateCompetencyUseCase _createCompetencyUseCase;
    private readonly IDeleteCompetencyUseCase _deleteCompetencyUseCase;
    private readonly IDeleteProgramOfStudyUseCase _deleteProgramOfStudyUseCase;
    private readonly IGetProgramOfStudiesUseCase _getProgramOfStudiesUseCase;
    private readonly IUpdateDraftV1CompetencyUseCase _updateDraftV1CompetencyUseCase;
    private readonly IUpdateProgramOfStudyUseCase _updateUseCase;
    private readonly IGetProgramOfStudyUseCase _getUseCase;
    private readonly IGetCompetencyUseCase _getCompetencyUseCase;
    private readonly IGetCompetenciesByProgramOfStudyUseCase _getCompetenciesByProgramOfStudyUseCase;
    private readonly UserControllerService _userControllerService;

    public ProgramOfStudyController(ICreateProgramOfStudyUseCase createUseCase,
                                    IDeleteProgramOfStudyUseCase deleteProgramOfStudyUseCase,
                                    IGetProgramOfStudyUseCase getProgramOfStudyUseCase,
                                    IGetProgramOfStudiesUseCase getProgramOfStudiesUseCase,
                                    IUpdateProgramOfStudyUseCase updateUseCase,
                                    ICreateCompetencyUseCase createCompetencyUseCase,
                                    IDeleteCompetencyUseCase deleteCompetencyUseCase,
                                    IUpdateDraftV1CompetencyUseCase updateDraftV1CompetencyUseCase,
                                    IGetCompetenciesByProgramOfStudyUseCase getCompetenciesByProgramOfStudyUseCase,
                                    IGetCompetencyUseCase getCompetencyUseCase,
                                    UserControllerService userControllerService)
    {
        _createUseCase = createUseCase;
        _deleteProgramOfStudyUseCase = deleteProgramOfStudyUseCase;
        _getProgramOfStudiesUseCase = getProgramOfStudiesUseCase;
        _deleteCompetencyUseCase = deleteCompetencyUseCase;
        _getUseCase = getProgramOfStudyUseCase;
        _updateUseCase=updateUseCase;
        _createCompetencyUseCase = createCompetencyUseCase;
        _updateDraftV1CompetencyUseCase = updateDraftV1CompetencyUseCase;
        _getCompetencyUseCase = getCompetencyUseCase;
        _getCompetenciesByProgramOfStudyUseCase = getCompetenciesByProgramOfStudyUseCase;
        _userControllerService = userControllerService;
    }

    #region ProgramOfStudy
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProgramOfStudyDTO>>> GetAll()
    {
        var programOfStudies = await _getProgramOfStudiesUseCase.Execute();
        return Ok(programOfStudies);
    }

    [Authorize(Roles = Roles.StudyProgram)]
    [HttpPost]
    public async Task<ActionResult<ProgramOfStudyDTO>> Create(ProgramOfStudyDTO programOfStudy)
    {
        return CreatedAtAction(
            nameof(Get),
            new { code = programOfStudy.Code },
            await _createUseCase.Execute(programOfStudy));
    }

    [Authorize]
    [HttpGet("{code}")]
    public async Task<IActionResult> Get(string code)
    {
        var program = await _getUseCase.Execute(code);
        return Ok(program);
    }

    [Authorize(Roles = Roles.StudyProgram)]
    [HttpPut("{code}")]
    public async Task<IActionResult> Update(string code, [FromBody] ProgramOfStudyDTO programOfStudyDTO)
    {
        var program = await _updateUseCase.Execute(code, programOfStudyDTO);
        return Ok(program);
    }

    [Authorize(Roles = Roles.StudyProgram)]
    [HttpDelete("{code}")]
    public async Task<IActionResult> Delete(string code)
    {
        await _deleteProgramOfStudyUseCase.Execute(code);
        return NoContent();
    }
    #endregion
    #region Competency
    [Authorize(Roles = Roles.Competency)]
    [HttpPost("{programOfStudyCode}/competency")]
    public async Task<ActionResult<CompetencyDTO>> AddCompetency(string programOfStudyCode, [FromBody] CompetencyDTO createCompetencyDTO)
    {
        User user = _userControllerService.GetUserFromHttpContext();
        CompetencyDTO competency = await _createCompetencyUseCase.Execute(programOfStudyCode, createCompetencyDTO, user);

        return CreatedAtAction(
            nameof(GetCompetency),
            new { programOfStudyCode, competencyCode = competency.Code },
            competency);
    }

    [Authorize(Roles = Roles.Competency)]
    [HttpGet("{programOfStudyCode}/competency/{competencyCode}")]
    public async Task<ActionResult<CompetencyDTO>> GetCompetency(string programOfStudyCode, string competencyCode)
    {
        CompetencyDTO competency = await _getCompetencyUseCase.Execute(programOfStudyCode, competencyCode);
        return Ok(competency);
    }

    [Authorize(Roles = Roles.Competency)]
    [HttpGet("{programOfStudyCode}/competency")]
    public async Task<ActionResult<IList<CompetencyDTO>>> GetCompetencies(string programOfStudyCode)
    {
        IList<CompetencyDTO> competencies = await _getCompetenciesByProgramOfStudyUseCase.Execute(programOfStudyCode);
        return Ok(competencies);
    }

    [Authorize(Roles = Roles.Competency)]
    [HttpPut("{programOfStudyCode}/competency/{competencyCode}")]
    public async Task<ActionResult<CompetencyDTO>> UpdateCompetency(string programOfStudyCode, string competencyCode, [FromBody] CompetencyDTO updateCompetencyDTO)
    {
        User user = _userControllerService.GetUserFromHttpContext();
        if (updateCompetencyDTO.VersionNumber == 1 && updateCompetencyDTO.IsDraft)
        {
            CompetencyDTO competency = await _updateDraftV1CompetencyUseCase.Execute(programOfStudyCode, competencyCode, updateCompetencyDTO, user);

            return Ok(competency);
        }
        else
        {
            throw new NotSupportedException("Not coded yet");
        }
    }

    [Authorize(Roles = Roles.Competency)]
    [HttpDelete("{programOfStudyCode}/competency/{competencyCode}")]
    public async Task<ActionResult> DeleteCompetency(string programOfStudyCode, string competencyCode)
    {
        await _deleteCompetencyUseCase.Execute(programOfStudyCode, competencyCode);
        return NoContent();
    }
    #endregion
}
