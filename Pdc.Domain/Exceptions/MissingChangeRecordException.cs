namespace Pdc.Domain.Exceptions;

public class MissingChangeRecordException : Exception
{
    public MissingChangeRecordException() : base()
    {
    }
    public MissingChangeRecordException(string message) : base(message)
    {
    }
    public MissingChangeRecordException(string message, Exception innerException) : base(message, innerException)
    {
    }
}