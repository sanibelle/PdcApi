namespace Pdc.Domain.Entities.Version;

public class MasterCoursePlanVersion : AVersion
{
    public required string Description { get; set; } // On a retiré xyz.

    //TODO une liste des modifications (CRUD) avec les différents changements effectués.
}
