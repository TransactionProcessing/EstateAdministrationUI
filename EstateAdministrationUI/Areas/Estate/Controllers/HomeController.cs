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

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [ExcludeFromCodeCoverage]
    [Authorize]
    [Area("Estate")]
    public class HomeController : Controller
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
        public HomeController(IApiClient apiClient,
                              IViewModelFactory viewModelFactory)
        {
            this.ApiClient = apiClient;
            this.ViewModelFactory = viewModelFactory;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        [Route("GetTodaysTransactionsAsJson")]
        public async Task<IActionResult> GetTodaysTransactionsAsJson(CancellationToken cancellationToken)
        {
            String accessToken = await this.HttpContext.GetTokenAsync("access_token");

            TransactionForPeriodModel transactionModel =
                await this.ApiClient.GetTransactionsForDatePeriod(accessToken, this.User.Identity as ClaimsIdentity, DatePeriod.Today, cancellationToken);

            TransactionPeriodViewModel viewModel = this.ViewModelFactory.ConvertFrom(transactionModel);

            return this.Json(viewModel);
        }

        [HttpPost]
        [Route("GetThisWeeksTransactionsAsJson")]
        public async Task<IActionResult> GetThisWeeksTransactionsAsJson(CancellationToken cancellationToken)
        {
            String accessToken = await this.HttpContext.GetTokenAsync("access_token");

            TransactionForPeriodModel transactionModel = await this.ApiClient.GetTransactionsForDatePeriod(accessToken, this.User.Identity as ClaimsIdentity, DatePeriod.ThisWeek, cancellationToken);

            TransactionPeriodViewModel viewModel = this.ViewModelFactory.ConvertFrom(transactionModel);

            return this.Json(viewModel);
        }

        [HttpPost]
        [Route("GetThisMonthsTransactionsAsJson")]
        public async Task<IActionResult> GetThisMonthsTransactionsAsJson(CancellationToken cancellationToken)
        {
            String accessToken = await this.HttpContext.GetTokenAsync("access_token");

            TransactionForPeriodModel transactionModel = await this.ApiClient.GetTransactionsForDatePeriod(accessToken, this.User.Identity as ClaimsIdentity, DatePeriod.ThisMonth, cancellationToken);

            TransactionPeriodViewModel viewModel = this.ViewModelFactory.ConvertFrom(transactionModel);

            return this.Json(viewModel);
        }

        #endregion
    }
}