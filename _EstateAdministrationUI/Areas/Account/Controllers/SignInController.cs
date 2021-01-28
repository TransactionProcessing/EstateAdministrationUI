namespace EstateAdministrationUI.Areas.Account.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Shared.General;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [ExcludeFromCodeCoverage]
    [Area("Account")]
    public class SignInController : Controller
    {
        #region Methods

        /// <summary>
        /// Loggeds the in.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> LoggedIn(CancellationToken cancellationToken)
        {
            // Decide which page we move to now based on the role
            return this.DetermineLoggedInView();
        }

        /// <summary>
        /// Logins the specified cancellation token.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Login(CancellationToken cancellationToken)
        {
            return this.RedirectToAction(nameof(this.LoggedIn));
        }

        /// <summary>
        /// Logouts the specified cancellation token.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        [HttpGet]
        [AllowAnonymous]
        public async Task Logout(CancellationToken cancellationToken)
        {
            await this.HttpContext.SignOutAsync("oidc");
            await this.HttpContext.SignOutAsync("Cookies");
        }

        /// <summary>
        /// Determines the logged in view.
        /// </summary>
        /// <returns></returns>
        private IActionResult DetermineLoggedInView()
        {
            IActionResult actionResult = null;

            Boolean isTestMode = bool.Parse(ConfigurationReader.GetValue("IsIntegrationTest"));

            if (isTestMode)
            {
                Boolean hasRole = this.User.HasClaim(c => c.Type == "role");
                List<Claim> roleNames = this.User.Claims.Where(c => c.Type == "role").ToList();

                foreach (Claim role in roleNames)
                {
                    if (role.Value.Contains("Merchant"))
                    {
                        // Merchant user
                        break;
                    }

                    if (role.Value.Contains("Estate"))
                    {
                        // Estate user
                        actionResult = this.RedirectToAction("Index",
                                                             "Home",
                                                             new
                                                             {
                                                                 Area = "Estate"
                                                             });
                        break;
                    }
                }
            }
            else
            {
                if (this.User.IsInRole("Estate"))
                {
                    actionResult = this.RedirectToAction("Index",
                                                         "Home",
                                                         new
                                                         {
                                                             Area = "Estate"
                                                         });
                }
            }

            return actionResult;
        }

        #endregion
    }
}