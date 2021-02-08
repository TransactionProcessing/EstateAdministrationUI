namespace EstateAdministrationUI.Areas.Estate.Controllers
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Security.Claims;
    using System.Threading;
    using System.Threading.Tasks;
    using BusinessLogic.Models;
    using Factories;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services;

    [ExcludeFromCodeCoverage]
    [Authorize]
    [Area("Estate")]
    public class ReportingController : Controller
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
        public ReportingController(IApiClient apiClient,
                                   IViewModelFactory viewModelFactory)
        {
            this.ApiClient = apiClient;
            this.ViewModelFactory = viewModelFactory;
        }

        #endregion

        #region Methods

        [HttpGet]
        public async Task<IActionResult> GetSalesByTimePeriodAsJson([FromQuery] String timePeriod,
                                                                    [FromQuery] DateTime startDate,
                                                                    [FromQuery] DateTime endDate,
                                                                    CancellationToken cancellationToken)
        {
            String accessToken = await this.HttpContext.GetTokenAsync("access_token");

            if (timePeriod == "Day")
            {
                TransactionsByDateModel model = await this.ApiClient.GetTransactionsByDate(accessToken,
                                                                                           this.User.Identity as ClaimsIdentity,
                                                                                           startDate,
                                                                                           endDate,
                                                                                           cancellationToken);
                TransactionsByDateViewModel viewModel = this.ViewModelFactory.ConvertFrom(model);
                return this.Json(viewModel);
            }

            if (timePeriod == "Week")
            {
                TransactionsByWeekModel model = await this.ApiClient.GetTransactionsByWeek(accessToken,
                                                                                           this.User.Identity as ClaimsIdentity,
                                                                                           startDate,
                                                                                           endDate,
                                                                                           cancellationToken);
                TransactionsByWeekViewModel viewModel = this.ViewModelFactory.ConvertFrom(model);
                return this.Json(viewModel);
            }

            if (timePeriod == "Month")
            {
                TransactionsByMonthModel model = await this.ApiClient.GetTransactionsByMonth(accessToken,
                                                                                             this.User.Identity as ClaimsIdentity,
                                                                                             startDate,
                                                                                             endDate,
                                                                                             cancellationToken);
                TransactionsByMonthViewModel viewModel = this.ViewModelFactory.ConvertFrom(model);
                return this.Json(viewModel);
            }

            return null;
        }

        [HttpGet]
        public async Task<IActionResult> GetSalesByMerchantAsJson([FromQuery] Int32 merchantCount,
                                                                  [FromQuery] SortDirection sortDirection,
                                                                  [FromQuery] SortField sortField,
                                                                    [FromQuery] DateTime startDate,
                                                                    [FromQuery] DateTime endDate,
                                                                    CancellationToken cancellationToken)
        {
            String accessToken = await this.HttpContext.GetTokenAsync("access_token");

            TransactionsByMerchantModel model = await this.ApiClient.GetTransactionsByMerchant(accessToken,
                                                                                               this.User.Identity as ClaimsIdentity,
                                                                                               startDate,
                                                                                               endDate,
                                                                                               merchantCount,
                                                                                               sortDirection,
                                                                                               sortField,
                                                                                               cancellationToken);

            TransactionsByMerchantViewModel viewModel = this.ViewModelFactory.ConvertFrom(model);
            return this.Json(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactionAnalysis(CancellationToken cancellationToken)
        {
            return this.View("TransactionAnalysis");
        }

        [HttpGet]
        public async Task<IActionResult> GetMerchantAnalysis(CancellationToken cancellationToken)
        {
            return this.View("MerchantAnalysis");
        }

        #endregion
    }
}