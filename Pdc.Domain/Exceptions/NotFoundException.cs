namespace Pdc.Domain.Exceptions;

public class NotFoundException : Exception
{
    public string TargetName { get; } = "";
    public string Id { get; } = "";
    public NotFoundException(string message) : base(message) { }
    public NotFoundException(string targetName, object id)
            : base($"{targetName} with id {id} was not found.")
    {
        TargetName = targetName;
        Id = id?.ToString() ?? "";
    }

}

