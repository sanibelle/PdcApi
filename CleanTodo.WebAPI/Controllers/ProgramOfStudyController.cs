using Microsoft.AspNetCore.Mvc;
using Pdc.Application.DTOS;
using Pdc.Application.Exceptions;
using Pdc.Application.UseCase;

[ApiController]
[Route("api/[controller]")]
public class ProgramOfStudyController : ControllerBase
{
    private ICreateProgramOfStudyUseCase _createUseCase;
    private IDeleteProgramOfStudyUseCase _deleteUseCase;
    private IGetAllProgramOfStudyUseCase _getAllUseCase;

    public ProgramOfStudyController(ICreateProgramOfStudyUseCase todoCreateUseCase,
                                    IDeleteProgramOfStudyUseCase deleteUseCase,
                                    IGetAllProgramOfStudyUseCase getAllUseCase)
    {
        _createUseCase = todoCreateUseCase;
        _deleteUseCase = deleteUseCase;
        _getAllUseCase = getAllUseCase;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProgramOfStudyDTO>>> GetAll()
    {
        var todos = await _getAllUseCase.Execute();
        return Ok(todos);
    }

    [HttpPost]
    public async Task<ActionResult<ProgramOfStudyDTO>> Create([FromBody] CreateProgramOfStudyDTO createProgramOfStudyDTO)
    {
        var todo = await _createUseCase.Execute(createProgramOfStudyDTO);

        return CreatedAtAction(
            nameof(Create),
            new { id = todo.Id },
            todo);
    }

    //[HttpPut("{id}")]
    //public async Task<IActionResult> Update(Guid id)
    //{
    //    try
    //    {
    //        return NoContent();
    //    }
    //    catch (NotFoundException)
    //    {
    //        return NotFound();
    //    }
    //}

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
