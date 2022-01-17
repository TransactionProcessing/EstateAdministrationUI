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

        /// <summary>
        /// The view model factory
        /// </summary>
        private readonly IViewModelFactory ViewModelFactory;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController" /> class.
        /// </summary>
        /// <param name="apiClient">The API client.</param>
        /// <param name="viewModelFactory">The view model factory.</param>
        public EstateController(IApiClient apiClient,
                                IViewModelFactory viewModelFactory)
        {
            this.ApiClient = apiClient;
            this.ViewModelFactory = viewModelFactory;
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

                EstateModel estateDetails = await this.ApiClient.GetEstate(accessToken, this.User.Identity as ClaimsIdentity, cancellationToken);

                return this.View("EstateDetails", this.ViewModelFactory.ConvertFrom(estateDetails));
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