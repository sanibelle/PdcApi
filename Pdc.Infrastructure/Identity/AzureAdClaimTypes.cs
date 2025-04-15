namespace Pdc.Infrastructure.Identity;

internal static class AzureAdClaimTypes
{
    internal const string ObjectId = "http://schemas.microsoft.com/identity/claims/objectidentifier";
    internal const string EmailAddress = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";
    internal const string GivenName = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname";
    internal const string SurName = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname";
    internal const string Name = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
}
