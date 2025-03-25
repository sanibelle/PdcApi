namespace Pdc.Domain.Entities.Versioning;

public abstract class Versionable
{
    public required ChangeRecord Version { get; set; }
}
