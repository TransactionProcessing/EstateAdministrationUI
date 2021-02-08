namespace EstateAdministrationUI.Common
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Security.Claims;

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
            // Find the claim first
            Claim? claim = identity.Claims.SingleOrDefault(c => c.Type == claimType);

            return (claim != null) ? claim.Value : string.Empty;
        }

        #endregion
    }
}