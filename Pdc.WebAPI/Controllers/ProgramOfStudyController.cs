using Microsoft.AspNetCore.Mvc;
using Pdc.Application.DTOS;
using Pdc.Application.Exceptions;
using Pdc.Application.UseCase;

namespace Pdc.WebAPI.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ProgramOfStudyController : ControllerBase
{
    private ICreateProgramOfStudyUseCase _createUseCase;
    private IDeleteProgramOfStudyUseCase _deleteUseCase;
    private IGetAllProgramOfStudyUseCase _getAllUseCase;
    private IUpdateProgramOfStudyUseCase _updateUseCase;
    private IGetProgramOfStudyUseCase _getUseCase;

    public ProgramOfStudyController(ICreateProgramOfStudyUseCase createUseCase,
                                    IDeleteProgramOfStudyUseCase deleteUseCase,
                                    IGetProgramOfStudyUseCase getUseCase,
                                    IGetAllProgramOfStudyUseCase getAllUseCase,
                                    IUpdateProgramOfStudyUseCase updateUseCase)
    {
        _createUseCase = createUseCase;
        _deleteUseCase = deleteUseCase;
        _getAllUseCase = getAllUseCase;
        _getUseCase = getUseCase;
        _updateUseCase=updateUseCase;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProgramOfStudyDTO>>> GetAll()
    {
        var programOfStudies = await _getAllUseCase.Execute();
        return Ok(programOfStudies);
    }

    [HttpPost]
    public async Task<ActionResult<ProgramOfStudyDTO>> Create([FromBody] UpsertProgramOfStudyDTO createProgramOfStudyDTO)
    {
        var programOfStudy = await _createUseCase.Execute(createProgramOfStudyDTO);

        return CreatedAtAction(
            nameof(Get),
            new { id = programOfStudy.Id },
            programOfStudy);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        try
        {
            var program = await _getUseCase.Execute(id);
            return Ok(program);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpsertProgramOfStudyDTO programOfStudyDTO)
    {
        try
        {
            var program = await _updateUseCase.Execute(id, programOfStudyDTO);
            return Ok(program);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _deleteUseCase.Execute(id);
            return NoContent();
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }
}
