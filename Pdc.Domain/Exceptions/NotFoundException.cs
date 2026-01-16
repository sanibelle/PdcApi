namespace Pdc.Domain.Exceptions;

public class NotFoundException : Exception
{
    public string TargetName { get; }
    public object Id { get; }
    public NotFoundException(string message) : base(message) { }
    public NotFoundException(string entityName, object id)
            : base($"{entityName} with id {id} was not found.")
    {
        TargetName = entityName;
        Id = id;
    }

}

