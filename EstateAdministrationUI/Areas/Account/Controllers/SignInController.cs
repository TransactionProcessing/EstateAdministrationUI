namespace EstateAdministrationUI.Areas.Account.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Shared.General;

    [Area("Account")]
    public class SignInController : Controller
    {
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Login(CancellationToken cancellationToken)
        {
            return this.RedirectToAction(nameof(this.LoggedIn));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> LoggedIn(CancellationToken cancellationToken)
        {
            // Decide which page we move to now based on the role
            return this.DetermineLoggedInView();
        }

        private IActionResult DetermineLoggedInView()
        {
            IActionResult actionResult = null;

            Boolean isTestMode = Boolean.Parse(ConfigurationReader.GetValue("IsIntegrationTest"));

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
                else
                {
                    // TODO: This should throw some kind of error as not supported
                }
            }

            return actionResult;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task Logout(CancellationToken cancellationToken)
        {
            await this.HttpContext.SignOutAsync("oidc");
            await this.HttpContext.SignOutAsync("Cookies");
        }
    }
}
