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
                DataByDateModel model = await this.ApiClient.GetTransactionsByDate(accessToken,
                                                                                   this.User.Identity as ClaimsIdentity,
                                                                                   startDate,
                                                                                   endDate,
                                                                                   cancellationToken);
                DataByDateViewModel viewModel = this.ViewModelFactory.ConvertFrom(model);
                return this.Json(viewModel);
            }

            if (timePeriod == "Week")
            {
                DataByWeekModel model = await this.ApiClient.GetTransactionsByWeek(accessToken,
                                                                                   this.User.Identity as ClaimsIdentity,
                                                                                   startDate,
                                                                                   endDate,
                                                                                   cancellationToken);
                DataByWeekViewModel viewModel = this.ViewModelFactory.ConvertFrom(model);
                return this.Json(viewModel);
            }

            if (timePeriod == "Month")
            {
                DataByMonthModel model = await this.ApiClient.GetTransactionsByMonth(accessToken,
                                                                                     this.User.Identity as ClaimsIdentity,
                                                                                     startDate,
                                                                                     endDate,
                                                                                     cancellationToken);
                DataByMonthViewModel viewModel = this.ViewModelFactory.ConvertFrom(model);
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

            DataByMerchantModel model = await this.ApiClient.GetTransactionsByMerchant(accessToken,
                                                                                       this.User.Identity as ClaimsIdentity,
                                                                                       startDate,
                                                                                       endDate,
                                                                                       merchantCount,
                                                                                       sortDirection,
                                                                                       sortField,
                                                                                       cancellationToken);

            DataByMerchantViewModel viewModel = this.ViewModelFactory.ConvertFrom(model);
            return this.Json(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetSettlementByMerchantAsJson([FromQuery] Int32 merchantCount,
                                                                  [FromQuery] SortDirection sortDirection,
                                                                  [FromQuery] SortField sortField,
                                                                  [FromQuery] DateTime startDate,
                                                                  [FromQuery] DateTime endDate,
                                                                  CancellationToken cancellationToken)
        {
            String accessToken = await this.HttpContext.GetTokenAsync("access_token");

            DataByMerchantModel model = await this.ApiClient.GetSettlementByMerchant(accessToken,
                                                                                     this.User.Identity as ClaimsIdentity,
                                                                                     startDate,
                                                                                     endDate,
                                                                                     merchantCount,
                                                                                     sortDirection,
                                                                                     sortField,
                                                                                     cancellationToken);

            DataByMerchantViewModel viewModel = this.ViewModelFactory.ConvertFrom(model);
            return this.Json(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetSettlementByTimePeriodAsJson([FromQuery] String timePeriod,
                                                                    [FromQuery] DateTime startDate,
                                                                    [FromQuery] DateTime endDate,
                                                                    CancellationToken cancellationToken)
        {
            String accessToken = await this.HttpContext.GetTokenAsync("access_token");

            if (timePeriod == "Day")
            {
                DataByDateModel model = await this.ApiClient.GetSettlementByDate(accessToken,
                                                                                   this.User.Identity as ClaimsIdentity,
                                                                                   startDate,
                                                                                   endDate,
                                                                                   cancellationToken);
                DataByDateViewModel viewModel = this.ViewModelFactory.ConvertFrom(model);
                return this.Json(viewModel);
            }

            if (timePeriod == "Week")
            {
                DataByWeekModel model = await this.ApiClient.GetSettlementByWeek(accessToken,
                                                                                   this.User.Identity as ClaimsIdentity,
                                                                                   startDate,
                                                                                   endDate,
                                                                                   cancellationToken);
                DataByWeekViewModel viewModel = this.ViewModelFactory.ConvertFrom(model);
                return this.Json(viewModel);
            }

            if (timePeriod == "Month")
            {
                DataByMonthModel model = await this.ApiClient.GetSettlementByMonth(accessToken,
                                                                                     this.User.Identity as ClaimsIdentity,
                                                                                     startDate,
                                                                                     endDate,
                                                                                     cancellationToken);
                DataByMonthViewModel viewModel = this.ViewModelFactory.ConvertFrom(model);
                return this.Json(viewModel);
            }

            return null;
        }

        [HttpGet]
        public async Task<IActionResult> GetSalesByOperatorAsJson([FromQuery] Int32 operatorCount,
                                                                  [FromQuery] SortDirection sortDirection,
                                                                  [FromQuery] SortField sortField,
                                                                  [FromQuery] DateTime startDate,
                                                                  [FromQuery] DateTime endDate,
                                                                  CancellationToken cancellationToken)
        {
            String accessToken = await this.HttpContext.GetTokenAsync("access_token");

            DataByOperatorModel model = await this.ApiClient.GetTransactionsByOperator(accessToken,
                                                                                       this.User.Identity as ClaimsIdentity,
                                                                                       startDate,
                                                                                       endDate,
                                                                                       operatorCount,
                                                                                       sortDirection,
                                                                                       sortField,
                                                                                       cancellationToken);

            DataByOperatorViewModel viewModel = this.ViewModelFactory.ConvertFrom(model);
            return this.Json(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetSettlementByOperatorAsJson([FromQuery] Int32 operatorCount,
                                                                  [FromQuery] SortDirection sortDirection,
                                                                  [FromQuery] SortField sortField,
                                                                  [FromQuery] DateTime startDate,
                                                                  [FromQuery] DateTime endDate,
                                                                  CancellationToken cancellationToken)
        {
            String accessToken = await this.HttpContext.GetTokenAsync("access_token");

            DataByOperatorModel model = await this.ApiClient.GetSettlementByOperator(accessToken,
                                                                                       this.User.Identity as ClaimsIdentity,
                                                                                       startDate,
                                                                                       endDate,
                                                                                       operatorCount,
                                                                                       sortDirection,
                                                                                       sortField,
                                                                                       cancellationToken);

            DataByOperatorViewModel viewModel = this.ViewModelFactory.ConvertFrom(model);
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

        [HttpGet]
        public async Task<IActionResult> GetOperatorAnalysis(CancellationToken cancellationToken)
        {
            return this.View("OperatorAnalysis");
        }

        #endregion
    }
}