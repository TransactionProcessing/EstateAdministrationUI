namespace EstateAdministrationUI.Areas.Estate.Controllers{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading;
    using System.Threading.Tasks;
    using BusinessLogic.Models;
    using Common;
    using EstateReportingAPI.Client;
    using EstateReportingAPI.DataTransferObjects;
    using EstateReportingAPI.DataTrasferObjects;
    using Factories;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [ExcludeFromCodeCoverage]
    [Authorize]
    [Area("Estate")]
    public class HomeController : Controller{
        #region Fields

        /// <summary>
        /// The API client
        /// </summary>
        private readonly IApiClient ApiClient;

        private readonly IConfigurationService ConfigurationService;

        #endregion

        #region Constructors

        public HomeController(IApiClient apiClient,
                              IConfigurationService configurationService){
            this.ApiClient = apiClient;
            this.ConfigurationService = configurationService;
        }

        #endregion

        #region Methods

        [HttpPost]
        public async Task<IActionResult> GetComparisonDatesAsJson(CancellationToken cancellationToken){
            String accessToken = await this.HttpContext.GetTokenAsync("access_token");

            Guid estateId = Helpers.GetClaimValue<Guid>(this.User.Identity as ClaimsIdentity, Helpers.EstateIdClaimType);

            List<ComparisonDateModel> response =
                await this.ApiClient.GetComparisonDates(accessToken, estateId, cancellationToken);

            List<(String value, String text)> viewModels = ViewModelFactory.ConvertFrom(response);

            return this.Json(viewModels);
        }

        [HttpPost]
        public async Task<IActionResult> GetComparisonDateSettlementAsJson(CancellationToken cancellationToken){
            String accessToken = await this.HttpContext.GetTokenAsync("access_token");

            Guid estateId = Helpers.GetClaimValue<Guid>(this.User.Identity as ClaimsIdentity, Helpers.EstateIdClaimType);

            DateTime comparisonDate = QueryStringHelper.GetDateTimeValueFromQueryString(this.Request.QueryString.Value, "comparisonDate", "yyyy-MM-dd");
            String comparisonDateLabel = QueryStringHelper.GetValueFromQueryString(this.Request.QueryString.Value, "comparisonDateLabel");

            TodaysSettlementModel response =
                await this.ApiClient.GetTodaysSettlement(accessToken, estateId, comparisonDate, cancellationToken);

            TodaysSettlementViewModel viewModel = ViewModelFactory.ConvertFrom(response, comparisonDateLabel);

            return this.Json(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> GetComparisonDateTransactionsAsJson(CancellationToken cancellationToken){
            String accessToken = await this.HttpContext.GetTokenAsync("access_token");

            Guid estateId = Helpers.GetClaimValue<Guid>(this.User.Identity as ClaimsIdentity, Helpers.EstateIdClaimType);

            DateTime comparisonDate = QueryStringHelper.GetDateTimeValueFromQueryString(this.Request.QueryString.Value, "comparisonDate", "yyyy-MM-dd");
            String comparisonDateLabel = QueryStringHelper.GetValueFromQueryString(this.Request.QueryString.Value, "comparisonDateLabel");

            TodaysSalesModel response =
                await this.ApiClient.GetTodaysSales(accessToken, estateId, comparisonDate, cancellationToken);

            TodaysSalesViewModel viewModel = ViewModelFactory.ConvertFrom(response, comparisonDateLabel);

            return this.Json(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> GetSalesCountByHourAsJson(CancellationToken cancellationToken){
            String accessToken = await this.HttpContext.GetTokenAsync("access_token");

            Guid estateId = Helpers.GetClaimValue<Guid>(this.User.Identity as ClaimsIdentity, Helpers.EstateIdClaimType);

            DateTime comparisonDate = QueryStringHelper.GetDateTimeValueFromQueryString(this.Request.QueryString.Value, "comparisonDate", "yyyy-MM-dd");

            List<TodaysSalesCountByHourModel> response =
                await this.ApiClient.GetTodaysSalesCountByHour(accessToken, estateId, comparisonDate, cancellationToken);

            List<HourCountViewModel> viewModels = ViewModelFactory.ConvertFrom(response);
            viewModels = viewModels.OrderBy(r => r.Hour).ToList();

            var model = new
                        {
                            transactionHourViewModels = viewModels
                        };

            return this.Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> GetSalesValueByHourAsJson(CancellationToken cancellationToken){
            String accessToken = await this.HttpContext.GetTokenAsync("access_token");

            Guid estateId = Helpers.GetClaimValue<Guid>(this.User.Identity as ClaimsIdentity, Helpers.EstateIdClaimType);

            DateTime comparisonDate = QueryStringHelper.GetDateTimeValueFromQueryString(this.Request.QueryString.Value, "comparisonDate", "yyyy-MM-dd");

            List<TodaysSalesValueByHourModel> response =
                await this.ApiClient.GetTodaysSalesValueByHour(accessToken, estateId, comparisonDate, cancellationToken);

            List<HourValueViewModel> viewModels = ViewModelFactory.ConvertFrom(response);
            viewModels = viewModels.OrderBy(r => r.Hour).ToList();

            var model = new
                        {
                            transactionHourViewModels = viewModels
            };

            return this.Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> GetYesterdaysSettlementAsJson(CancellationToken cancellationToken){
            String accessToken = await this.HttpContext.GetTokenAsync("access_token");

            Guid estateId = Helpers.GetClaimValue<Guid>(this.User.Identity as ClaimsIdentity, Helpers.EstateIdClaimType);

            TodaysSettlementModel response = await this.ApiClient.GetTodaysSettlement(accessToken, estateId, DateTime.Now, cancellationToken);

            TodaysSettlementViewModel viewModel = ViewModelFactory.ConvertFrom(response);

            return this.Json(viewModel);
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index(){
            return this.View();
        }

        #endregion
    }
}