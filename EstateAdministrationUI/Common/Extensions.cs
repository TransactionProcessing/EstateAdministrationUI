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

        /// <summary>
        /// Gets the claim value.
        /// </summary>
        /// <param name="identity">The identity.</param>
        /// <param name="claimType">Type of the claim.</param>
        /// <returns></returns>
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

        #endregion
    }
}