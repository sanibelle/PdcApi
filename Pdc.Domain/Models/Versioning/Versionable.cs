namespace Pdc.Domain.Models.Versioning;

public abstract class Versionable
{
    public required ChangeRecord CurrentVersion { get; set; }
}
