namespace Pdc.Application.DTOS;

public class CreateCompetencyDTO
{
    public required string Code { get; set; }
    public bool IsMandatory { get; set; }
    public bool IsOptionnal { get; set; }
    public required string StatementOfCompetency { get; set; }

    public CreateCompetencyDTO() { }
}
