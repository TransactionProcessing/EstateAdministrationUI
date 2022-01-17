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

        /// <summary>
        /// Gets the merchant analysis.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetMerchantAnalysis(CancellationToken cancellationToken)
        {
            return this.View("MerchantAnalysis");
        }

        /// <summary>
        /// Gets the operator analysis.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetOperatorAnalysis(CancellationToken cancellationToken)
        {
            return this.View("OperatorAnalysis");
        }

        /// <summary>
        /// Gets the sales by merchant as json.
        /// </summary>
        /// <param name="merchantCount">The merchant count.</param>
        /// <param name="sortDirection">The sort direction.</param>
        /// <param name="sortField">The sort field.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets the sales by operator as json.
        /// </summary>
        /// <param name="operatorCount">The operator count.</param>
        /// <param name="sortDirection">The sort direction.</param>
        /// <param name="sortField">The sort field.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets the sales by time period as json.
        /// </summary>
        /// <param name="timePeriod">The time period.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets the settlement by merchant as json.
        /// </summary>
        /// <param name="merchantCount">The merchant count.</param>
        /// <param name="sortDirection">The sort direction.</param>
        /// <param name="sortField">The sort field.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets the settlement by operator as json.
        /// </summary>
        /// <param name="operatorCount">The operator count.</param>
        /// <param name="sortDirection">The sort direction.</param>
        /// <param name="sortField">The sort field.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets the settlement by time period as json.
        /// </summary>
        /// <param name="timePeriod">The time period.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets the transaction analysis.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetTransactionAnalysis(CancellationToken cancellationToken)
        {
            return this.View("TransactionAnalysis");
        }

        #endregion
    }
}