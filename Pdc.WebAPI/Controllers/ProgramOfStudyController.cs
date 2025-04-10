using Microsoft.AspNetCore.Mvc;
using Pdc.Application.DTOS;
using Pdc.Application.UseCase;

namespace Pdc.WebAPI.Controllers;
[ApiController]
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

    public ProgramOfStudyController(ICreateProgramOfStudyUseCase createUseCase,
                                    IDeleteProgramOfStudyUseCase deleteUseCase,
                                    IGetProgramOfStudyUseCase getUseCase,
                                    IGetAllProgramOfStudyUseCase getAllUseCase,
                                    IUpdateProgramOfStudyUseCase updateUseCase,
                                    ICreateCompetencyUseCase createCompetencyUseCase,
                                    IGetCompetencyUseCase getCompetencyUseCase)
    {
        _createUseCase = createUseCase;
        _deleteUseCase = deleteUseCase;
        _getAllUseCase = getAllUseCase;
        _getUseCase = getUseCase;
        _updateUseCase=updateUseCase;
        _createCompetencyUseCase = createCompetencyUseCase;
        _getCompetencyUseCase = getCompetencyUseCase;
    }

    #region ProgramOfStudy
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProgramOfStudyDTO>>> GetAll()
    {
        var programOfStudies = await _getAllUseCase.Execute();
        return Ok(programOfStudies);
    }

    [HttpPost]
    public async Task<ActionResult<ProgramOfStudyDTO>> Create([FromBody] ProgramOfStudyDTO createProgramOfStudyDTO)
    {
        var programOfStudy = await _createUseCase.Execute(createProgramOfStudyDTO);
        return CreatedAtAction(
            nameof(Get),
            new { code = programOfStudy.Code },
            programOfStudy);
    }

    [HttpGet("{code}")]
    public async Task<IActionResult> Get(string code)
    {
        var program = await _getUseCase.Execute(code);
        return Ok(program);
    }

    [HttpPut("{code}")]
    public async Task<IActionResult> Update(string code, [FromBody] ProgramOfStudyDTO programOfStudyDTO)
    {
        var program = await _updateUseCase.Execute(code, programOfStudyDTO);
        return Ok(program);
    }

    [HttpDelete("{code}")]
    public async Task<IActionResult> Delete(string code)
    {
        await _deleteUseCase.Execute(code);
        return NoContent();
    }
    #endregion
    #region Competency
    [HttpPost("{programOfStudyCode}/competency")]
    public async Task<ActionResult<CompetencyDTO>> AddCompetency(string programOfStudyCode, [FromBody] CompetencyDTO createCompetencyDTO)
    {
        CompetencyDTO competency = await _createCompetencyUseCase.Execute(programOfStudyCode, createCompetencyDTO);

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
