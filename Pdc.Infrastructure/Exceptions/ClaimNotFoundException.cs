namespace Pdc.Infrastructure.Exceptions
{
    public class ClaimNotFoundException : Exception
    {
        public ClaimNotFoundException(string claimName)
            : base($"The required claim '{claimName}' was not found in the provided claims")
        {
        }
    }
}