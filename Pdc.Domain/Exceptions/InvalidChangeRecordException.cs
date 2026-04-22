namespace Pdc.Domain.Exceptions;

public class InvalidChangeRecordException : Exception
{
    public InvalidChangeRecordException() : base()
    {
    }
    public InvalidChangeRecordException(string message) : base(message)
    {
    }
    public InvalidChangeRecordException(string message, Exception innerException) : base(message, innerException)
    {
    }
}