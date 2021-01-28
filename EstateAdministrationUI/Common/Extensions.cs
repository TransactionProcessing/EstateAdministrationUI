namespace EstateAdministrationUI.Common
{
    using System;
    using System.Linq;
    using System.Security.Claims;

    public static class Extensions
    {
        public static String GetClaimValue(this ClaimsIdentity identity,
                                         String claimType)
        {
            // Find the claim first
            Claim? claim = identity.Claims.SingleOrDefault(c => c.Type == claimType);

            return (claim != null) ? claim.Value : String.Empty;
        }
    }
}
