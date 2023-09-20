namespace EstateAdministrationUI.Common
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Security.Claims;
    using Shared.Logger;

    /// <summary>
    /// 
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class Extensions
    {
        #region Methods

        public static String GetClaimValue(this ClaimsIdentity identity,
                                           String claimType)
        {
            var claims = identity.Claims;
            Logger.LogInformation($"Getting Claim [{claimType}]");
            foreach (Claim claim1 in claims)
            {
                Logger.LogInformation($"Claim [{claim1.Type}] Value [{claim1.Value}]");
            }

            // Find the claim first
            Claim? claim = identity.Claims.SingleOrDefault(c => c.Type.ToLower() == claimType.ToLower());

            return (claim != null) ? claim.Value : string.Empty;
        }

        public static Decimal SafeDivision(this Decimal numerator, Decimal denominator)
        {
            return (denominator == 0) ? 0 : numerator / denominator;
        }

        #endregion
    }
}