using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstateAdministrationUI.Areas.Estate.Controllers
{
    using System.Security.Claims;
    using System.Threading;
    using BusinessLogic.Models;
    using EstateAdministrationUI.Services;
    using Factories;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models;

    [Authorize]
    [Area("Estate")]
    public class HomeController :Controller
    {
        private readonly IApiClient ApiClient;

        private readonly IViewModelFactory ViewModelFactory;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController" /> class.
        /// </summary>
        public HomeController(IApiClient apiClient, IViewModelFactory viewModelFactory)
        {
            this.ApiClient = apiClient;
            this.ViewModelFactory = viewModelFactory;
        }

        #endregion

        [HttpGet]
        public IActionResult Index()
        {
            return this.View();
        }

        [HttpGet]
        public async Task<IActionResult> ManageEstate(CancellationToken cancellationToken)
        {
            String accessToken = await this.HttpContext.GetTokenAsync("access_token");

            EstateModel estateDetails = await this.ApiClient.GetEstate(accessToken, this.User.Identity as ClaimsIdentity, cancellationToken);
            
            return this.View(this.ViewModelFactory.ConvertFrom(estateDetails));
        }

        [HttpPost]
        public async Task<IActionResult> ManageEstate(EstateViewModel viewModel, CancellationToken cancellationToken)
        {
            return this.View(viewModel);
        }
    }
}
