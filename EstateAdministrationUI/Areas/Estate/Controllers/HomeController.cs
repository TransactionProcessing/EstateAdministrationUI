namespace EstateAdministrationUI.Areas.Estate.Controllers
{
    using System;
    using System.Collections.Generic;
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

        /// <summary>
        /// Gets the todays transactions as json.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets the this weeks transactions as json.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetThisWeeksTransactionsAsJson")]
        public async Task<IActionResult> GetThisWeeksTransactionsAsJson(CancellationToken cancellationToken)
        {
            String accessToken = await this.HttpContext.GetTokenAsync("access_token");

            TransactionForPeriodModel transactionModel = await this.ApiClient.GetTransactionsForDatePeriod(accessToken, this.User.Identity as ClaimsIdentity, DatePeriod.ThisWeek, cancellationToken);

            TransactionPeriodViewModel viewModel = this.ViewModelFactory.ConvertFrom(transactionModel);

            return this.Json(viewModel);
        }

        /// <summary>
        /// Gets the number of merchants as json.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetNumberOfMerchantsAsJson")]
        public async Task<IActionResult> GetNumberOfMerchantsAsJson(CancellationToken cancellationToken)
        {
            String accessToken = await this.HttpContext.GetTokenAsync("access_token");

            List<MerchantModel> merchants = await this.ApiClient.GetMerchants(accessToken, this.User.Identity as ClaimsIdentity, cancellationToken);

            MerchantCountViewModel viewModel = this.ViewModelFactory.ConvertFrom(merchants.ToArray());

            return this.Json(viewModel);
        }

        /// <summary>
        /// Gets the this months transactions as json.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetThisMonthsTransactionsAsJson")]
        public async Task<IActionResult> GetThisMonthsTransactionsAsJson(CancellationToken cancellationToken)
        {
            String accessToken = await this.HttpContext.GetTokenAsync("access_token");

            TransactionForPeriodModel transactionModel = await this.ApiClient.GetTransactionsForDatePeriod(accessToken, this.User.Identity as ClaimsIdentity, DatePeriod.ThisMonth, cancellationToken);

            TransactionPeriodViewModel viewModel = this.ViewModelFactory.ConvertFrom(transactionModel);

            return this.Json(viewModel);
        }

        /// <summary>
        /// Gets the transactions by merchant as json.
        /// </summary>
        /// <param name="merchantCount">The merchant count.</param>
        /// <param name="sortDirection">The sort direction.</param>
        /// <param name="sortField">The sort field.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetTransactionsByMerchantAsJson")]
        public async Task<IActionResult> GetTransactionsByMerchantAsJson([FromQuery] Int32 merchantCount,
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

        /// <summary>
        /// Gets the transactions by operator as json.
        /// </summary>
        /// <param name="operatorCount">The operator count.</param>
        /// <param name="sortDirection">The sort direction.</param>
        /// <param name="sortField">The sort field.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetTransactionsByOperatorAsJson")]
        public async Task<IActionResult> GetTransactionsByOperatorAsJson([FromQuery] Int32 operatorCount,
                                                                  [FromQuery] SortDirection sortDirection,
                                                                  [FromQuery] SortField sortField,
                                                                  [FromQuery] DateTime startDate,
                                                                  [FromQuery] DateTime endDate,
                                                                  CancellationToken cancellationToken)
        {
            String accessToken = await this.HttpContext.GetTokenAsync("access_token");

            TransactionsByOperatorModel model = await this.ApiClient.GetTransactionsByOperator(accessToken,
                                                                                               this.User.Identity as ClaimsIdentity,
                                                                                               startDate,
                                                                                               endDate,
                                                                                               operatorCount,
                                                                                               sortDirection,
                                                                                               sortField,
                                                                                               cancellationToken);

            TransactionsByOperatorViewModel viewModel = this.ViewModelFactory.ConvertFrom(model);
            return this.Json(viewModel);
        }

        #endregion
    }
}