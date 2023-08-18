namespace EstateAdministrationUI.Areas.Estate.Controllers
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Security.Claims;
    using System.Threading;
    using System.Threading.Tasks;
    using BusinessLogic.Models;
    using Common;
    using Factories;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services;

    [ExcludeFromCodeCoverage]
    [Authorize]
    [Area("Estate")]
    public class EstateController : Controller
    {
        #region Fields

        /// <summary>
        /// The API client
        /// </summary>
        private readonly IApiClient ApiClient;
        

        #endregion

        #region Constructors
        
        public EstateController(IApiClient apiClient)
        {
            this.ApiClient = apiClient;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Manages the estate.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetEstate(CancellationToken cancellationToken)
        {
            try
            {
                String accessToken = await this.HttpContext.GetTokenAsync("access_token");

                Guid estateId = Helpers.GetClaimValue<Guid>(this.User.Identity as ClaimsIdentity, Helpers.EstateIdClaimType);

                EstateModel estateDetails = await this.ApiClient.GetEstate(accessToken, Guid.Empty,
                                                                           estateId, cancellationToken);

                return this.View("EstateDetails", ViewModelFactory.ConvertFrom(estateDetails));
            }
            catch(Exception ex)
            {
                // Something went wrong creating the contract
                return this.View("EstateDetails").WithWarning("Estate Details", Helpers.BuildUserErrorMessage("Error getting estate information"));
            }
        }

        /// <summary>
        /// Manages the estate.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetEstate(EstateViewModel viewModel,
                                                   CancellationToken cancellationToken)
        {
            // TODO: Update the estate information

            return this.View("EstateDetails", viewModel);
        }

        #endregion
    }
}