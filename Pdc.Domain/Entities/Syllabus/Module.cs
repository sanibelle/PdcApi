namespace Pdc.Domain.Entities.Syllabus;

public class Module
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required int Position { get; set; }
}
