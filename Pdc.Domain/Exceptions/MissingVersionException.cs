namespace Pdc.Domain.Exceptions;

public class MissingVersionException : Exception
{
    public MissingVersionException() : base()
    {
    }
    public MissingVersionException(string message) : base(message)
    {
    }
    public MissingVersionException(string message, Exception innerException) : base(message, innerException)
    {
    }
}