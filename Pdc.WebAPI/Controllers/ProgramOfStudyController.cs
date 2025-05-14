using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pdc.Application.DTOS;
using Pdc.Application.UseCase;
using Pdc.Domain.Models.Security;
using Pdc.Infrastructure.Identity;
using Pdc.WebAPI.Services;

namespace Pdc.WebAPI.Controllers;
[ApiController]
[Authorize]
[Route("api/[controller]")]
public class ProgramOfStudyController : ControllerBase
{
    private ICreateProgramOfStudyUseCase _createUseCase;
    private ICreateCompetencyUseCase _createCompetencyUseCase;
    private IDeleteProgramOfStudyUseCase _deleteUseCase;
    private IGetAllProgramOfStudyUseCase _getAllUseCase;
    private IUpdateProgramOfStudyUseCase _updateUseCase;
    private IGetProgramOfStudyUseCase _getUseCase;
    private IGetCompetencyUseCase _getCompetencyUseCase;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private UserControllerService _userControllerService;

    public ProgramOfStudyController(ICreateProgramOfStudyUseCase createUseCase,
                                    IDeleteProgramOfStudyUseCase deleteUseCase,
                                    IGetProgramOfStudyUseCase getUseCase,
                                    IGetAllProgramOfStudyUseCase getAllUseCase,
                                    IUpdateProgramOfStudyUseCase updateUseCase,
                                    ICreateCompetencyUseCase createCompetencyUseCase,
                                    IGetCompetencyUseCase getCompetencyUseCase,
                                    UserControllerService userControllerService,
                                    IHttpContextAccessor httpContextAccessor)
    {
        _createUseCase = createUseCase;
        _deleteUseCase = deleteUseCase;
        _getAllUseCase = getAllUseCase;
        _getUseCase = getUseCase;
        _updateUseCase=updateUseCase;
        _createCompetencyUseCase = createCompetencyUseCase;
        _getCompetencyUseCase = getCompetencyUseCase;
        _userControllerService = userControllerService;
        _httpContextAccessor=httpContextAccessor;
        _httpContextAccessor = httpContextAccessor;
    }

    #region ProgramOfStudy
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProgramOfStudyDTO>>> GetAll()
    {
        var programOfStudies = await _getAllUseCase.Execute();
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
        await _deleteUseCase.Execute(code);
        return NoContent();
    }
    #endregion
    #region Competency
    //TODO ROLES
    [Authorize(Roles = Roles.Competency)]
    [HttpPost("{programOfStudyCode}/competency")]
    public async Task<ActionResult<CompetencyDTO>> AddCompetency(string programOfStudyCode, [FromBody] CompetencyDTO createCompetencyDTO)
    {
        User user = _userControllerService.GetUserFromHttpContext(_httpContextAccessor);
        CompetencyDTO competency = await _createCompetencyUseCase.Execute(programOfStudyCode, createCompetencyDTO, user);

        return CreatedAtAction(
            nameof(GetCompetency),
            new { programOfStudyCode, competencyCode = competency.Code },
            competency);
    }


    [HttpGet("{programOfStudyCode}/competency/{competencyCode}")]
    public async Task<ActionResult<CompetencyDTO>> GetCompetency(string programOfStudyCode, string competencyCode)
    {
        CompetencyDTO competency = await _getCompetencyUseCase.Execute(programOfStudyCode, competencyCode);
        return Ok(competency);
    }
    #endregion
}
