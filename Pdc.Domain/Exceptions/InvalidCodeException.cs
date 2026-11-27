namespace Pdc.Domain.Exceptions;

public class InvalidCodeException : Exception
{
    public InvalidCodeException() : base()
    {
    }
    public InvalidCodeException(string message) : base(message)
    {
    }
    public InvalidCodeException(string message, Exception innerException) : base(message, innerException)
    {
    }
}